namespace ExpressMapperCore;
public readonly record struct MapKey(Type SourceType, Type DestType)
{
    public static MapKey Form<TSource, TDest>()
    {
        return new MapKey(typeof(TSource), typeof(TDest));
    }
}

public interface IKeyKeeper
{
    public MapKey Key { get; }
}