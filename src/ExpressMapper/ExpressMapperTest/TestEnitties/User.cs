namespace ExpressMapperTest.TestEnitties;

public class User
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string AnotherDescription { get; init; }
}

public class UserDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}