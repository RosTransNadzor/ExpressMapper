using System.ComponentModel;
using System.Linq.Expressions;

namespace ExpressMapperCore.Configuration.Clause;

public enum MemberOwner
{
    Source,
    Destination
}

public interface IIngnoreClause<TSoure, TDest> : IClause<TSoure,TDest>
{
    IgnoreClauseDestinationEx GetDestinationIgnoreMember();
    IgnoreClauseSourceEx GetSourceIgnoreMember();
}
public record IgnoreClauseDestinationEx(MemberExpression? DestinationMember);
public record IgnoreClauseSourceEx(MemberExpression? SourceMember);
public class IngnoreClause<TSource,TDest,TMember> : IReverseAbleClause<TSource,TDest>,
    IIngnoreClause<TSource,TDest>
{
    private readonly Expression<Func<TSource, TMember>>? _sourceMember;
    private readonly Expression<Func<TDest, TMember>>? _destMember;
    private readonly MemberOwner Owner;
    
    public bool IsValidClause { get; }
    
    public IngnoreClause(Expression<Func<TSource,TMember>> sourceMember)
    {
        _sourceMember = sourceMember;
        Owner = MemberOwner.Source;
        IsValidClause = DefineIfValidClause(sourceMember);
    }

    public IngnoreClause(Expression<Func<TDest, TMember>> destMember)
    {
        _destMember = destMember;
        Owner = MemberOwner.Destination;
        IsValidClause = DefineIfValidClause(destMember);
    }
    
    private bool DefineIfValidClause
        (Expression<Func<TDest, TMember>> dest)
    {
        return dest.Body.NodeType is ExpressionType.MemberAccess;
    }
    private bool DefineIfValidClause
        (Expression<Func<TSource, TMember>> dest)
    {
        return dest.Body.NodeType is ExpressionType.MemberAccess;
    }

    public IClause<TDest, TSource> GetReverseClause()
    {
        switch (Owner)
        {
            case MemberOwner.Destination:
                return new IngnoreClause<TDest, TSource, TMember>(_destMember!);
            case MemberOwner.Source:
                return new IngnoreClause<TDest, TSource, TMember>(_sourceMember!);
            default:
                throw new InvalidEnumArgumentException("ignoreclause owner invalid value");
        }
    }

    public IgnoreClauseDestinationEx GetDestinationIgnoreMember()
    {
        return new IgnoreClauseDestinationEx(_destMember?.Body as MemberExpression);
    }

    public IgnoreClauseSourceEx GetSourceIgnoreMember()
    {
        return new IgnoreClauseSourceEx(_sourceMember?.Body as MemberExpression);
    }
}