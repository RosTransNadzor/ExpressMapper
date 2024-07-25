using System.Reflection;

namespace ExpressMapperCore.Expressions;

public class MemberForMapping
{
    public required string MemberName { get; init; }
    public required MemberInfo MemberInfo { get; init; }
    public required Type MemberType { get; init; }
    
}