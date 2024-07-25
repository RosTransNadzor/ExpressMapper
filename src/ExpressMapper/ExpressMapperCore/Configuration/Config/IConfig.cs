namespace ExpressMapperCore.Configuration;

public interface IConfig : IKeyKeeper;

public interface IConfig<TSource, TDest> : IConfig
{
    IEnumerable<IClause<TSource,TDest>> Clauses { get; }
}
public class Config<TSource, TDest> : IConfig<TSource,TDest>
{
    public IEnumerable<IClause<TSource, TDest>> Clauses { get; }
    public MapKey Key => MapKey.Form<TSource,TDest>();

    public Config
        (IEnumerable<IClause<TSource,TDest>> cluases)
    {
        Clauses = cluases;
    }
}