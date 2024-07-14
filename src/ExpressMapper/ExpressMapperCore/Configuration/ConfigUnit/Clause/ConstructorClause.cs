using System.Reflection;

namespace ExpressMapperCore.Configuration.Clause;

public class ConstructorClause<TSource,TDest> : IClause<TSource,TDest>
{
    public ConstructorInfo? ConstructorInfo { get; }
    public bool IsValidClause { get; }

    public ConstructorClause(ConstructorInfo? info)
    {
        ConstructorInfo = info;
        IsValidClause = DefineIfValidClause(ConstructorInfo);
    }

    private bool DefineIfValidClause(ConstructorInfo? constructorInfo)
    {
        return constructorInfo?.IsPublic ?? false;
    }
}