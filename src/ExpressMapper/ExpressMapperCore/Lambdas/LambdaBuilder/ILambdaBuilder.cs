using ExpressMapperCore.Expressions;
using ExpressMapperCore.MapBuilder.MapExpression;

namespace ExpressMapperCore.Lambdas.LambdaBuilder;

public interface ILambdaBuilder
{
    MapLambda<TSource,TDest> BuildLambda<TSource, TDest>();
}
public class LambdaBuilder : ILambdaBuilder
{
    private readonly IMapExpressionBuilder _expressionBuilder;

    public LambdaBuilder(IMapExpressionBuilder expressionBuilder)
    {
        _expressionBuilder = expressionBuilder;
    }
    public MapLambda<TSource, TDest> BuildLambda<TSource, TDest>()
    {
        return new MapLambda<TSource, TDest> { Lambda = BuildLambdaCore<TSource, TDest>() };
    }

    private Func<TSource,TDest> BuildLambdaCore<TSource, TDest>()
    {
        return _expressionBuilder.FormExpression<TSource, TDest>().Compile();
    }
}