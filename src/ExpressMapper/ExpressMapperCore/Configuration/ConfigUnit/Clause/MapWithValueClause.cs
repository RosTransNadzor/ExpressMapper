using System.Linq.Expressions;
using ExpressMapperCore.Expressions;

namespace ExpressMapperCore.Configuration.Clause;

public interface IMapMemberWithValueClause<TSource, TDest> : IClause<TSource,TDest>
{
    MapClauseWithValueEx? GetMemberExpression();
}

public record MapClauseWithValueEx(MemberExpression MemberExpression,object? Value);
public class MapWithValueClause<TSource,TDest,TMember> : IMapMemberWithValueClause<TSource,TDest>
{
    private readonly Expression<Func<TDest, TMember>> _destAccessExpression;
    private readonly TMember _memberValue;
    
    public bool IsValidClause { get; }

    public MapWithValueClause
        (Expression<Func<TDest, TMember>> destExpression,TMember value)
    {
        _destAccessExpression = destExpression;
        _memberValue = value;
        IsValidClause = DefineIfValidClause(destExpression);
    }

    private bool DefineIfValidClause
        (Expression<Func<TDest, TMember>> dest)
    {
        if (dest.Body is MemberExpression memberExpression)
        {
            var name = memberExpression.Member.Name;
            
            //ensure destination member is writeable
            return MemberSearchHelper.FindMember
                (typeof(TDest),name,typeof(TMember),true) is not null;
        }

        return false;
    }

    public MapClauseWithValueEx? GetMemberExpression()
    {
        if (!IsValidClause)
            return null;
        
        return new MapClauseWithValueEx(
            (_destAccessExpression.Body as MemberExpression)!,
            _memberValue
        );
    }
}