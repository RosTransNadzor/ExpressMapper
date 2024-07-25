using System.Reflection;
using ExpressMapperCore.Expressions;

namespace ExpressMapperCore.Configuration.Config.Clause;

public interface IConstructClause<TSorce, TDest> : IClause<TSorce, TDest>
{
    public ConstructorInfo? ConstructorInfo { get; }
    public IEnumerable<MappingMember> ConstructorParams { get;}
}
public class ConstructorClause<TSource,TDest> : IConstructClause<TSource,TDest>
{
    private readonly ICollection<MappingMember> _ctorParams = [];
    public ConstructorInfo? ConstructorInfo { get; }
    public IEnumerable<MappingMember> ConstructorParams => _ctorParams;
    public bool IsValidClause { get; }

    public ConstructorClause(ConstructorInfo? info)
    {
        ConstructorInfo = info;
        IsValidClause = DefineIfValidClause(ConstructorInfo);
        if (IsValidClause)
        {
            InitConstructorParams();
        }
    }

    private bool DefineIfValidClause(ConstructorInfo? constructorInfo)
    {
        return constructorInfo?.IsPublic ?? false;
    }

    private void InitConstructorParams()
    {
        var parameters = ConstructorInfo!.GetParameters();
        foreach (var parameter in parameters)
        {
            _ctorParams.Add(new MappingMember
            {
                Info = parameter.Member,
                Name = parameter.Name!,
                Type = parameter.ParameterType
            });
        }
    }
}