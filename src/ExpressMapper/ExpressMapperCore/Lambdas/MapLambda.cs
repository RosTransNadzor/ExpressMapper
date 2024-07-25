namespace ExpressMapperCore.MapBuilder.MapExpression;

public interface IMapLambda : IKeyKeeper;

public interface IMapLambda<TSource, TDest> : IMapLambda
{
    public Func<TSource,TDest> Lambda { get; }
}
public class MapLambda<TSource,TDest> : IMapLambda<TSource,TDest>
{
    public required Func<TSource,TDest> Lambda { get; init; }
    public MapKey Key => MapKey.Form<TSource, TDest>();
}

