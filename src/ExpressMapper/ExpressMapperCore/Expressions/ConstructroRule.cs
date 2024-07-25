
using System.Reflection;

namespace ExpressMapperCore.Expressions;

public class ConstructroRule : IMappingRule
{
    public required Tuple<Type,MappingMember?>[] ConstructorParams { get; init; }
    public required ConstructorInfo Info { get; init; }
}