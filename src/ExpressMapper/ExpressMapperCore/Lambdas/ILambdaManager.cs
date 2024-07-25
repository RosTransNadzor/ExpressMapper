using ExpressMapperCore.Configuration;
using ExpressMapperCore.Lambdas;
using ExpressMapperCore.Lambdas.LambdaBuilder;
using ExpressMapperCore.MapBuilder.MapExpression;

namespace ExpressMapperCore.MapBuilder;

public interface ILambdaManager
{
    Func<TSource, TDest> GetLambda<TSource, TDest>();
}
public class LambdaManager : ILambdaManager
{
    private readonly ILambdaCache _cache;
    private readonly ILambdaBuilder _lambdaBuilder;
    private readonly IConfigManager _configManager;
    public LambdaManager(ILambdaCache cache, ILambdaBuilder lambdaBuilder, IConfigManager configManager)
    {
        _cache = cache;
        _lambdaBuilder = lambdaBuilder;
        _configManager = configManager;
    }
    public Func<TSource, TDest> GetLambda<TSource,TDest>()
    {
        return GetMapLambda<TSource, TDest>().Lambda;
    }

    private IMapLambda<TSource, TDest> GetMapLambda<TSource, TDest>()
    {
        return 
            _cache.GetOrAdd(
                ConstructLambda<TSource, TDest>
            );
    }
    private IMapLambda<TSource,TDest> ConstructLambda<TSource, TDest>()
        => _lambdaBuilder.BuildLambda<TSource,TDest>(_configManager);
}