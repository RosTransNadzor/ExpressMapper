using ExpressMapperCore.Lambdas.LambdaBuilder;
using ExpressMapperCore.MapBuilder.MapExpression;

namespace ExpressMapperCore.MapBuilder;

public interface ILambdaManager
{
    Func<TSource, TDest> GetLambda<TSource, TDest>();
    void ClearLambdas();
}
public class LambdaManager : ILambdaManager
{
    private readonly IMapCache<IMapLambda> _cache;
    private readonly ILambdaBuilder _lambdaBuilder;
    public LambdaManager(IMapCache<IMapLambda> cache, ILambdaBuilder lambdaBuilder)
    {
        _cache = cache;
        _lambdaBuilder = lambdaBuilder;
    }
    public Func<TSource, TDest> GetLambda<TSource,TDest>()
    {
        return GetMapLambda<TSource, TDest>().Lambda;
    }

    public void ClearLambdas()
    {
        _cache.Clear();
    }

    private MapLambda<TSource, TDest> GetMapLambda<TSource, TDest>()
    {
        return (MapLambda<TSource, TDest>)
            _cache.GetOrAdd(
                MapKey.Form<TSource, TDest>(),
                ConstructLambda<TSource, TDest>
            );
    }
    private MapLambda<TSource,TDest> ConstructLambda<TSource, TDest>()
        => _lambdaBuilder.BuildLambda<TSource,TDest>();
}