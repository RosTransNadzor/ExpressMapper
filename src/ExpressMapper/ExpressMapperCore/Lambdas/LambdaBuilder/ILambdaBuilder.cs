using ExpressMapperCore.Configuration;
using ExpressMapperCore.Expressions;
using ExpressMapperCore.MapBuilder.MapExpression;

namespace ExpressMapperCore.Lambdas.LambdaBuilder;

public interface ILambdaBuilder
{
    MapLambda<TSource,TDest> BuildLambda<TSource, TDest>(IConfigManager configManager);
}
public class LambdaBuilder : ILambdaBuilder
{
    private readonly IMapExpressionBuilder _expressionBuilder;

    public LambdaBuilder(IMapExpressionBuilder expressionBuilder)
    {
        _expressionBuilder = expressionBuilder;
    }
    public MapLambda<TSource, TDest> BuildLambda<TSource, TDest>(IConfigManager configManager)
    {
        return new MapLambda<TSource, TDest> { Lambda = BuildLambdaCore<TSource, TDest>(configManager) };
    }

    private Func<TSource,TDest> BuildLambdaCore<TSource, TDest>(IConfigManager configManager)
    {
        return _expressionBuilder.FormExpression<TSource, TDest>(configManager).Compile();
    }
}