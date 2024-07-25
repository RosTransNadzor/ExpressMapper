using System.Collections.Concurrent;
using ExpressMapperCore.MapBuilder.MapExpression;

namespace ExpressMapperCore.Lambdas;

public interface ILambdaCache
{
    public IMapLambda<TSource,TDest> GetOrAdd<TSource, TDest>(Func<IMapLambda<TSource, TDest>> _faultFunc);
}
public class LambdaCache : ILambdaCache
{
    private readonly ConcurrentDictionary<MapKey, IMapLambda> _lambdas;

    public LambdaCache()
    {
        _lambdas = new ConcurrentDictionary<MapKey, IMapLambda>();
    }

    public IMapLambda<TSource,TDest> GetOrAdd<TSource, TDest>(Func<IMapLambda<TSource, TDest>> faultFunc)
    {
        var key = MapKey.Form<TSource, TDest>();
        return (IMapLambda<TSource,TDest>) _lambdas.GetOrAdd(key, _ => faultFunc());
    }
}