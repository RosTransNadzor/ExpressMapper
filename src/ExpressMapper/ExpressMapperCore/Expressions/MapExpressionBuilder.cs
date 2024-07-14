using System.Linq.Expressions;
using ExpressMapperCore.Configuration;

namespace ExpressMapperCore.Expressions;

public interface IMapExpressionBuilder
{
    Expression<Func<TSource, TDest>> FormExpression<TSource,TDest>();
}

public class MapExpressionBuilder: IMapExpressionBuilder
{
    public Expression<Func<TSource, TDest>> FormExpression<TSource,TDest>()
    {
        var config = ConfigManager.GetConfigUnit<TSource, TDest>();
        ParameterExpression parameter = FormParameter<TSource>();

        var body = new DestinationCreator()
            .CreateDestination(config,parameter)
            .AsignMembersByClauses()
            .AsignMemberByAuto()
            .ToExpression();

        return Expression.Lambda<Func<TSource, TDest>>(body, parameter);
    }

    private ParameterExpression FormParameter<TSource>()
    {
        return Expression.Parameter(typeof(TSource), nameof(TSource).ToLower());
    }
    
}