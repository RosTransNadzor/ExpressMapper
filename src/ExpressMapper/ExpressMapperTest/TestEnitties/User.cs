namespace ExpressMapperTest.TestEnitties;

public class User
{
    public int Age { get; init; }
    public string Name { get; set; }
    public string Description { get; init; }
    public string AnotherDescription { get; init; }
}

public class UserDto
{
    public int Age { get; init; }
    public string Name { get; }
    public readonly string AnotherDescription;
    public string Description { get; init; }
}