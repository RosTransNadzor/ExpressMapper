using System.Linq.Expressions;
using ExpressMapperCore.Expressions;

namespace ExpressMapperCore.Configuration.Clause;

public interface IMapMemberClause<TSource,TDest> : IClause<TSource,TDest>
{
    MapClauseEx? GetMemberExpressions();
}

public record MapClauseEx(MemberExpression SourceMember, MemberExpression DestMember);
public class MapWithMemberClause<TSource,TDest,TMember> : IReverseAbleClause<TSource,TDest>,
    IMapMemberClause<TSource,TDest>
{
    private readonly Expression<Func<TSource,TMember>> _sourceAccessExpression;
    private readonly Expression<Func<TDest, TMember>> _destAccessExpression;

    public bool IsValidClause { get; }

    public MapWithMemberClause
        (Expression<Func<TSource, TMember>> sourceExpression, Expression<Func<TDest, TMember>> destExpression)
    {
        _sourceAccessExpression = sourceExpression;
        _destAccessExpression = destExpression;
        IsValidClause = DefineIfValidClause(sourceExpression,destExpression);
    }

    private bool DefineIfValidClause
        (Expression<Func<TSource, TMember>> source, Expression<Func<TDest, TMember>> dest)
    {
        if (dest.Body is MemberExpression destMemberEx && source.Body is MemberExpression sourceMemberEx)
        {
            var destName = destMemberEx.Member.Name;

            // ensure member is writeable for destination
            return MemberSearchHelper.FindMember(typeof(TDest), destName, typeof(TMember), true)
                is not null;
        }
        return false;
    }

    public IClause<TDest, TSource> GetReverseClause()
    {
        return new MapWithMemberClause<TDest, TSource, TMember>
            (_destAccessExpression, _sourceAccessExpression);
    }

    public MapClauseEx? GetMemberExpressions()
    {
        if (!IsValidClause)
            return null;
        
        return new MapClauseEx(
            (_sourceAccessExpression.Body as MemberExpression)!,
            (_destAccessExpression.Body as MemberExpression)!
        );
    }
}