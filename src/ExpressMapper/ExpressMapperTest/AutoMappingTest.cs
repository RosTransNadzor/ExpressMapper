using ExpressMapperCore.Mapper;
using ExpressMapperTest.TestEnitties;

namespace ExpressMapperTest;

public class AutoMappingTest
{
    [Fact]
    public void AutoMappingByName()
    {
        var mapper = Mapper.Create();
        var user = new User
        {
            Description = "desc",
            Age = 45
        };
        var dto = mapper.Map<User,UserDto>(user);
        Assert.True(dto.Description == user.Description);
        Assert.True(dto.Age == user.Age);
    }

    [Fact]
    public void CannotMapDestPropertyWithoutSetter()
    {
        var mapper = Mapper.Create();
        var user = new User
        {
            Name = "name"
        };
        var dto = mapper.Map<User, UserDto>(user);
        Assert.True(dto.Name is null);
    }
    [Fact]
    public void CannotMapDestReadonlyField()
    {
        var mapper = Mapper.Create();
        var user = new User
        {
            AnotherDescription = "another"
        };
        var dto = mapper.Map<User, UserDto>(user);
        Assert.True(dto.AnotherDescription is null);
    }
}