using ExpressMapperCore.Configuration.Clause;

namespace ExpressMapperCore.Configuration.ConfigUnit.Clause;

public class ClauseHelper
{
    public static bool IsConfigHasIgnoreClauseForSource<TSource, TDest>
        (IConfigUnit<TSource,TDest>? config,string memberName)
    {
        return config?.Clauses
            .OfType<IIngnoreClause<TSource, TDest>>()
            .Where(c => c.IsValidClause)
            .Select(c => c.GetSourceIgnoreMember())
            .Any(m => m.SourceMember?.Member.Name == memberName) ?? false;
    }
    public static bool IsConfigHasIgnoreClauseForDest<TSource, TDest>
        (IConfigUnit<TSource,TDest>? config,string memberName)
    {
        return config?.Clauses
            .OfType<IIngnoreClause<TSource, TDest>>()
            .Where(c => c.IsValidClause)
            .Select(c => c.GetDestinationIgnoreMember())
            .Any(m => m.DestinationMember?.Member.Name == memberName) ?? false;
    }

    public static IEnumerable<MapClauseEx> FindMembersInMapClauses<TSource, TDest>
        (IConfigUnit<TSource,TDest> config)
    {
        return config.Clauses
            .OfType<IMapMemberClause<TSource, TDest>>()
            .Where(c => c.IsValidClause)
            .Select(c => c.GetMemberExpressions()!);
    }

    public static IEnumerable<MapClauseWithValueEx> FindMemberInMapValueClauses<TSource, TDest>
        (IConfigUnit<TSource, TDest> config)
    {
        return config.Clauses
            .OfType<IMapMemberWithValueClause<TSource, TDest>>()
            .Where(c => c.IsValidClause)
            .Select(c => c.GetMemberExpression()!);
    }
}