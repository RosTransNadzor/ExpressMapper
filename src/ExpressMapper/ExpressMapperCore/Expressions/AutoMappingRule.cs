namespace ExpressMapperCore.Expressions;

public class AutoMappingRule : IMappingRule
{
    public required MappingMember SourceMember { get; init; }
    public required MappingMember DestinationMember { get; init; }
}