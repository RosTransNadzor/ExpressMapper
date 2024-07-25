namespace ExpressMapper;

public class User
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UserDto
{
    public string UserName { get; set; }
    public Guid Id { get; set; }
}