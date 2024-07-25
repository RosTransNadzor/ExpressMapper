using System.Linq.Expressions;
using ExpressMapperCore.Configuration;

namespace ExpressMapperCore.Expressions;

public interface IMapExpressionBuilder
{
    Expression<Func<TSource, TDest>> FormExpression<TSource, TDest>(IConfigManager configManager);
}

public class MapExpressionBuilder: IMapExpressionBuilder
{
    private readonly IExpressionBuilder _expressionBuilder;

    public MapExpressionBuilder(IExpressionBuilder expressionBuilder)
    {
        _expressionBuilder = expressionBuilder;
    }

    public Expression<Func<TSource, TDest>> FormExpression<TSource, TDest>(IConfigManager configManager)
    {
        var config = configManager.GetConfig<TSource, TDest>();
        var mappingTracker = LibraryFactory.Instance.CreateMappingTracker(config)
            .RemoveIgnored()
            .MapConstructorParams()
            .AutoMapByName()
            .MapByClauses();

        return _expressionBuilder.BuildExpression<TSource, TDest>(mappingTracker.GetMappingRules());
    }
}