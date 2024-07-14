namespace ExpressMapperCore.Configuration;

public interface IConfigUnit : IKeyKeeper
{
    public IConfigUnit? ReverseWayUnit { get; }
}

public interface IConfigUnit<TSource, TDest> : IConfigUnit
{
    public ICollection<IClause<TSource,TDest>> Clauses { get; }
}
public class ConfigUnit<TSource, TDest> : IConfigUnit<TSource,TDest>
{
    public ICollection<IClause<TSource, TDest>> Clauses { get; }
    public MapKey Key => MapKey.Form<TSource,TDest>();
    public IConfigUnit? ReverseWayUnit { get; }

    public ConfigUnit
        (ICollection<IClause<TSource,TDest>> cluases,ConfigUnit<TDest, TSource>? reverseWayUnit = null)
    {
        Clauses = cluases;
        ReverseWayUnit = reverseWayUnit;
    }
}