using System.Reflection;

namespace ExpressMapperCore.Expressions;

public record MappingMember
{
    public required string Name { get; init; }
    public required Type Type { get; init; }
    public required MemberInfo Info { get; init; }
}