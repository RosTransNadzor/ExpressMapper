using ExpressMapperCore.Expressions;
using ExpressMapperCore.Lambdas.LambdaBuilder;
using ExpressMapperCore.MapBuilder;
using ExpressMapperCore.MapBuilder.MapExpression;

namespace ExpressMapperCore.Mapper;

public class Mapper : IMapper
{
    private static readonly ILambdaManager _manager;
    static Mapper()
    {
        _manager = new LambdaManager
            (new KeyKeeperCollection<IMapLambda>(),new LambdaBuilder(new MapExpressionBuilder()));
    }
    
    public static IMapper CreateMapper()
    {
        return new Mapper();
    }

    public TDest Map<TSource, TDest>(TSource source)
    {
        Func<TSource, TDest> func = _manager.GetLambda<TSource, TDest>();
        return func(source);
    }
}