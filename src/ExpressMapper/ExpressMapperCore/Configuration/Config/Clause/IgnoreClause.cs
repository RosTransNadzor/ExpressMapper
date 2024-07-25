
using System.Linq.Expressions;
using ExpressMapperCore.Expressions;

namespace ExpressMapperCore.Configuration.Config.Clause;

public interface IIgnoreClause<TSource, TDest> : IClause<TSource, TDest>
{
    public MappingMember? SourceIgnoreMember { get; }
    public MappingMember? DestinationIgnoreMember { get; }
}
public class IgnoreClause<TSource,TDest,TMember> : IIgnoreClause<TSource,TDest>
{
    public MappingMember? SourceIgnoreMember { get; }
    public MappingMember? DestinationIgnoreMember { get; }
    public bool IsValidClause { get; }

    public IgnoreClause(Expression<Func<TSource,TMember>> sourceExpression)
    {
        if (DefineIfValidClause(sourceExpression))
        {
            var member = (MemberExpression) sourceExpression.Body;
            var name = member.Member.Name;
            var type = typeof(TMember);
            SourceIgnoreMember = new MappingMember
            {
                Info = member.Member,
                Type = type,
                Name = name
            };
            IsValidClause = true;
        }
    }

    public IgnoreClause(Expression<Func<TDest, TMember>> destinationExpression)
    {
        if (DefineIfValidClause(destinationExpression))
        {
            var member = (MemberExpression) destinationExpression.Body;
            var name = member.Member.Name;
            var type = typeof(TMember);
            DestinationIgnoreMember = new MappingMember
            {
                Info = member.Member,
                Type = type,
                Name = name
            };
            IsValidClause = true;
        }
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
}