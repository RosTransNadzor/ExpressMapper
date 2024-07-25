using System.Linq.Expressions;

namespace ExpressMapperCore.Expressions;

public class MapClauseRule : IMappingRule
{
    public required MappingMember DestinationMember { get; init; }
    public required LambdaExpression SourceLambda { get; init; }
}