namespace ExpressMapperCore.MapBuilder.MapExpression;

public interface IMapLambda : IKeyKeeper;
public class MapLambda<TSource,TDest> : IMapLambda
{
    public required Func<TSource,TDest> Lambda { get; init; }
    public MapKey Key => MapKey.Form<TSource, TDest>();
}

