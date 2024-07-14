using System.Linq.Expressions;
using ExpressMapperCore.Configuration;
using ExpressMapperCore.Configuration.ConfigUnit.Clause;

namespace ExpressMapperCore.Expressions;

public class DestinationType<TSource,TDest>
{
    private readonly ParameterExpression _parameter;
    private readonly Expression _destVariable;
    private readonly Expression _creation;
    private readonly IConfigUnit<TSource, TDest>? _configUnit;
    private readonly IList<MemberForMapping> _alreadyDestAssigned;

    public DestinationType(
            Expression destVariableEx,
            Expression creationExpression,
            IConfigUnit<TSource,TDest>? configUnit,
            IList<MemberForMapping> list, ParameterExpression parameter)
    {
        _destVariable = destVariableEx;
        _creation = creationExpression;
        _configUnit = configUnit;
        _alreadyDestAssigned = list;
        _parameter = parameter;
    }

    public DestinationWithAssignedMembersByClauses<TSource,TDest> AsignMembersByClauses()
    {
        return new DestinationWithAssignedMembersByClauses<TSource,TDest>(
            _destVariable,
            _alreadyDestAssigned,
            AssignMembersByClausesCore(),
            _creation,
            _configUnit,
            _parameter
        );
    }

    private IEnumerable<Expression> AssignMembersByClausesCore()
    {
        if(_configUnit is null)
            yield break;
        
        foreach (var assingMemberByMapMember in AssingMemberByMapMemberClauses())
            yield return assingMemberByMapMember;
        
        foreach (var assingMembersByValue in AssingMembersByValueClauses())
            yield return assingMembersByValue;
    }

    private IEnumerable<Expression> AssingMembersByValueClauses()
    {
        var mapClausesWithValue = ClauseHelper.FindMemberInMapValueClauses(_configUnit!);

        foreach (var clause in mapClausesWithValue)
        {
            var info = clause.MemberExpression.Member;
            if (_alreadyDestAssigned
                .All(mfm => mfm.MemberName != info.Name)
                && !ClauseHelper.IsConfigHasIgnoreClauseForDest(_configUnit,info.Name)
               )
            {
                
                _alreadyDestAssigned.Add(new MemberForMapping
                {
                    MemberInfo = info,
                    MemberName = info.Name,
                    MemberType = clause.MemberExpression.Type
                });

                var value = Expression.Constant(clause.Value);
                var destVarMember = Expression.PropertyOrField(_destVariable, info.Name);
                yield return Expression.Assign(destVarMember, value);
            }
        }
    }

    private IEnumerable<Expression> AssingMemberByMapMemberClauses()
    {
        var mapClauses = ClauseHelper.FindMembersInMapClauses(_configUnit!);

        foreach (var clause in mapClauses)
        {
            var sourceInfo = clause.SourceMember.Member;
            var destInfo = clause.DestMember.Member;

            if (
                _alreadyDestAssigned.All(mfm => mfm.MemberName != destInfo.Name)
                && !ClauseHelper.IsConfigHasIgnoreClauseForSource(_configUnit,sourceInfo.Name)
                && !ClauseHelper.IsConfigHasIgnoreClauseForDest(_configUnit,destInfo.Name)
            )
            {
                _alreadyDestAssigned.Add(new MemberForMapping
                {
                    MemberInfo = destInfo,
                    MemberName = destInfo.Name,
                    MemberType = clause.DestMember.Type
                });

                var destVarMember = Expression.PropertyOrField(_destVariable, destInfo.Name);
                var sourceVarMember = Expression.PropertyOrField(_parameter, sourceInfo.Name);
                yield return Expression.Assign(destVarMember,sourceVarMember);
            }
        }
    }
}