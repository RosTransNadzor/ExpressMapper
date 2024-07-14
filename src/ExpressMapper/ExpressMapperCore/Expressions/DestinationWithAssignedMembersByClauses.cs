using System.Linq.Expressions;
using ExpressMapperCore.Configuration;
using ExpressMapperCore.Configuration.ConfigUnit.Clause;

namespace ExpressMapperCore.Expressions;

public class DestinationWithAssignedMembersByClauses<TSource,TDest>
{
    private readonly ParameterExpression _parameter;
    private readonly Expression _destVariable;
    private readonly Expression _creationExpression;
    private readonly IEnumerable<Expression> _assignByClauseExpressions;
    private readonly IList<MemberForMapping> _alreadyDestAssignedMembers;
    private readonly IConfigUnit<TSource, TDest>? _configUnit;

    public DestinationWithAssignedMembersByClauses(
        Expression destVariableExpression,
        IList<MemberForMapping> alreadyDestAssignedMembers, 
        IEnumerable<Expression> assignByClauseExpressions, 
        Expression creationExpression, IConfigUnit<TSource, TDest>? configUnit, 
        ParameterExpression parameter
    )
    {
        _destVariable = destVariableExpression;
        _alreadyDestAssignedMembers = alreadyDestAssignedMembers;
        _assignByClauseExpressions = assignByClauseExpressions;
        _creationExpression = creationExpression;
        _configUnit = configUnit;
        _parameter = parameter;
    }

    public DestinationWithFullAsigning AsignMemberByAuto()
    {
        return new DestinationWithFullAsigning(
            _destVariable,
            _creationExpression,
            GetFullAssigns()
        );
    }

    private IEnumerable<Expression> GetFullAssigns()
    {
        foreach (var assignment in _assignByClauseExpressions)
            yield return assignment;
        foreach (var assignment in AssingMembersNameEquality())
            yield return assignment;
    }

    private IEnumerable<Expression> AssingMembersNameEquality()
    {
        foreach (var destMemberInfo in MemberSearchHelper.FindAllMembers(typeof(TDest),isWriteable:true))
        {
            var sourceMemberInfo = MemberSearchHelper.FindMember
                (typeof(TSource), destMemberInfo.MemberName, destMemberInfo.MemberType, false);
            
            if(sourceMemberInfo is null) continue;
            
            if (_alreadyDestAssignedMembers.All
                    (mfm => mfm.MemberName != destMemberInfo.MemberName)
                && !ClauseHelper.IsConfigHasIgnoreClauseForDest(_configUnit,destMemberInfo.MemberName)
                && !ClauseHelper.IsConfigHasIgnoreClauseForSource(_configUnit,sourceMemberInfo.MemberName))
            {
                var destVarMember = Expression.PropertyOrField(_destVariable, destMemberInfo.MemberName);
                var sourceVarMember = Expression.PropertyOrField(_parameter, sourceMemberInfo.MemberName);
                yield return Expression.Assign(destVarMember, sourceVarMember);
            }
                
        }
    }
}