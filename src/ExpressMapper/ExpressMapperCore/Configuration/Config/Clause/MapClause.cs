using System.Linq.Expressions;
using ExpressMapperCore.Expressions;
namespace ExpressMapperCore.Configuration.Config.Clause;
public interface IMapClause<TSource, TDest> : IClause<TSource, TDest>
{
    public MappingMember? DestinationMember { get; }
    public LambdaExpression? Expression { get; }
}
public class MapClause<TSource,TDest,TMember> : IMapClause<TSource,TDest>
{
    public bool IsValidClause { get; }
    public MappingMember? DestinationMember { get; }
    public LambdaExpression? Expression { get; }

    public MapClause(Expression<Func<TDest,TMember>> destMemberAccess,
        Expression<Func<TSource,TMember>> lambdaExpression)
    {
        if (DefineIfValidMemberAccess(destMemberAccess))
        {
            var member = (MemberExpression) destMemberAccess.Body;
            var name = member.Member.Name;
            var type = typeof(TMember);
            DestinationMember = new MappingMember
            {
                Info = member.Member,
                Type = type,
                Name = name
            };
            Expression = lambdaExpression;
            IsValidClause = true;
        }
    }

    private bool DefineIfValidMemberAccess(Expression<Func<TDest,TMember>> destMemberAccess)
    {
        return destMemberAccess.Body is MemberExpression;
    }
}