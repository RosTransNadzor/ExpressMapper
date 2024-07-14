namespace ExpressMapperCore.Configuration;

public interface IClause<TSource, TDest>
{
    public bool IsValidClause { get; }
}

public interface IReverseAbleClause<TSource, TDest> : IClause<TSource,TDest>
{
    public IClause<TDest, TSource> GetReverseClause();
}