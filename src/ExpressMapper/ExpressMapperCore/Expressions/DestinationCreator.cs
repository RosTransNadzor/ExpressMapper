using System.Linq.Expressions;
using System.Reflection;
using ExpressMapperCore.Configuration;
using ExpressMapperCore.Configuration.Clause;
using ExpressMapperCore.Configuration.ConfigUnit.Clause;

namespace ExpressMapperCore.Expressions;
public class DestinationCreator
{
    public DestinationType<TSource,TDest> CreateDestination<TSource, TDest>
        (IConfigUnit<TSource, TDest>? config,ParameterExpression source)
    {
        var info = FindSuitableConstructor(config);
        var ctorParams = FormConstructorParams<TDest>(info);

        var alreadyDestAssigned = new List<MemberForMapping>();
        var paramExpressions = FormAssingExpressions(source,ctorParams,config,alreadyDestAssigned);
        
        return new DestinationType<TSource,TDest>(
            Expression.Variable(typeof(TDest),nameof(TDest).ToLower()),
            Expression.New(info,paramExpressions),
            config,
            alreadyDestAssigned,
            source
        );
    }
    private ConstructorInfo FindSuitableConstructor<TSource,TDest>
        (IConfigUnit<TSource,TDest>? configUnit)
    {
         return GetFirstConstuctorClause(configUnit)?.ConstructorInfo 
                ?? FindConstructorWithoutParams<TDest>();
    }

    private ConstructorClause<TSource,TDest>? GetFirstConstuctorClause<TSource, TDest>
        (IConfigUnit<TSource, TDest>? configUnit)
    {
        if (configUnit is null)
            return default;
        
        return configUnit.Clauses
            .OfType<ConstructorClause<TSource, TDest>>()
            .FirstOrDefault(cc => cc.IsValidClause);
    }

    private ConstructorInfo FindConstructorWithoutParams<TDest>()
    {
        return typeof(TDest).GetConstructor([])
               ?? throw new Exception($"type {typeof(TDest)} does not have public constructor wihtout params");
    }

    private MemberForMapping[] FormConstructorParams<TDest>(ConstructorInfo info)
    {
        return info.GetParameters()
            .Select(pi => new MemberForMapping
            {
                MemberInfo = pi.Member,
                MemberType = pi.ParameterType,
                MemberName = NameHelper.ToUpperFirstLatter(pi.Name!)
            }).ToArray();
    }

    private IEnumerable<Expression> FormAssingExpressions<TSource,TDest>(
        ParameterExpression source,
        MemberForMapping[] ctorParams,
        IConfigUnit<TSource,TDest>? config,
        IList<MemberForMapping> alreadyDestAssigned)
    {
        foreach (var param in ctorParams)
        {
            if (
                ClauseHelper.IsConfigHasIgnoreClauseForDest(config,param.MemberName) ||
                ClauseHelper.IsConfigHasIgnoreClauseForSource(config,param.MemberName)
            )
            {
                yield return ExpressionHelper.DefaultValue(param.MemberType);
                continue;
            }
            
            var memberExpression = MemberSearchHelper
                .FormMemberAccess(source, param.MemberName, param.MemberType);

            if (memberExpression is null)
            {
                yield return ExpressionHelper.DefaultValue(param.MemberType);
                continue;
            }
            
            alreadyDestAssigned.Add(param);
            yield return memberExpression;
        }
    }
}