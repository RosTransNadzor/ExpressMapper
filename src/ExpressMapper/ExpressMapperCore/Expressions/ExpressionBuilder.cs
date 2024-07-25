using System.Linq.Expressions;
using ExpressMapperCore.Exceptions;

namespace ExpressMapperCore.Expressions;

public interface IExpressionBuilder
{
    public Expression<Func<TSource, TDest>> BuildExpression<TSource, TDest>(IEnumerable<IMappingRule> rules);
}
public class ExpressionBuilder : IExpressionBuilder
{

    public Expression<Func<TSource, TDest>> BuildExpression<TSource, TDest>(IEnumerable<IMappingRule> rules)
    {
        var rulesArray = rules.ToArray();
        var source = FormParameter<TSource>();
        var destination = FormVariable<TDest>();
        var assignProperties = AssignProperties(destination, source, rulesArray);
        var body = Expression.Block([(ParameterExpression) destination],assignProperties);
        
        return Expression.Lambda<Func<TSource, TDest>>(body, source);
    }

    private Expression FormVariable<TDest>()
    {
        return Expression.Variable(typeof(TDest), nameof(TDest).ToLower());
    }
    private ParameterExpression FormParameter<TSource>()
    {
        return Expression.Parameter(typeof(TSource), nameof(TSource).ToLower());
    }

    private Expression NewDestination
        (Type destinationType,ParameterExpression source,IEnumerable<IMappingRule> rules)
    {
        var ctorRule = rules.OfType<ConstructroRule>()
            .FirstOrDefault();
        if (ctorRule is not null)
        {
            var paramsExpressions = new Expression[ctorRule.ConstructorParams.Length];
            int count = 0;
            foreach (var param in ctorRule.ConstructorParams)
            {
                if (param.Item2 is not null)
                {
                    paramsExpressions[count] = Expression.PropertyOrField(source, param.Item2.Name);
                }
                else
                {
                    paramsExpressions[count] = ExpressionHelper.DefaultValue(param.Item1);
                }

                count++;
            }

            return Expression.New(ctorRule.Info, paramsExpressions);
        }
        
        var ctor = destinationType.GetConstructor([]);
        if (ctor is null)
            throw new CannotFindConstructorWithoutParamsException(destinationType);

        return Expression.New(ctor, []);
    }

    private IEnumerable<Expression> AssignProperties
        (Expression destination,ParameterExpression source,IEnumerable<IMappingRule> rules)
    {
        var mappedRules = rules.ToArray();
        yield return Expression.Assign
            (destination, NewDestination(destination.Type, source, mappedRules));
        foreach (var autoAssignExpression in 
                 AssingAutoProperies(destination,source,mappedRules.OfType<AutoMappingRule>()))
            yield return autoAssignExpression;
        foreach (var expression in 
                 AssingMappedByClauseProperties(destination, source, mappedRules.OfType<MapClauseRule>()))
        {
            yield return expression;
        }

        yield return destination;

    }

    private IEnumerable<Expression> AssingAutoProperies
        (Expression destination,ParameterExpression source,IEnumerable<IMappingRule> rules)
    {
        var autoMappingRules = rules.OfType<AutoMappingRule>();
        foreach (var autoMappingRule in autoMappingRules)
        {
            var destMember = Expression.PropertyOrField(destination, autoMappingRule.DestinationMember.Name);
            var sourceMember = Expression.PropertyOrField(source, autoMappingRule.SourceMember.Name);

            yield return Expression.Assign(destMember, sourceMember);
        }
    }

    private IEnumerable<Expression> AssingMappedByClauseProperties
        (Expression destination, ParameterExpression source, IEnumerable<IMappingRule> rules)
    {
        var mappedByClause = rules.OfType<MapClauseRule>();
        foreach (var mapRule in mappedByClause)
        {
            var destMember = Expression.PropertyOrField(destination, mapRule.DestinationMember.Name);
            var lambda = mapRule.SourceLambda;
            var lambdaCall = Expression.Invoke(lambda, source);
            yield return Expression.Assign(destMember, lambdaCall);
        }
    }
}