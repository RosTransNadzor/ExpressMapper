using ExpressMapperCore.Configuration;
using ExpressMapperCore.Mapper;
using ExpressMapperTest.TestEnitties;

namespace ExpressMapperTest;
file class IgnoreUserConfig : MapperConfig<User, UserDto>
{
    public override void Configure(IMappingConfigurer<User, UserDto> configurer)
    {
        configurer
            .IgnoreDest(dto => dto.Description)
            .IgnoreSource(user => user.Age);
    }
}

file class IgnoreToConstructor : MapperConfig<Address, AddressDto>
{
    public override void Configure(IMappingConfigurer<Address, AddressDto> configurer)
    {
        configurer
            .WithConstructor<string, string, string>()
            .IgnoreSource(adress => adress.City)
            .IgnoreDest(dto => dto.Country);
    }
}
public class IgnoreMemberTest
{
    [Fact]
    public void DestMemberIgnored()
    {
        var mapper = Mapper.Create<IgnoreUserConfig>();
        var user = new User
        {
            Description = "desc"
        };
        var dto = mapper.Map<User, UserDto>(user);
        Assert.True(dto.Description is null);
    }

    [Fact]
    public void SourceMemberIgnored()
    {
        var mapper = Mapper.Create<IgnoreUserConfig>();
        var user = new User
        {
            Age = 45
        };
        var dto = mapper.Map<User, UserDto>(user);
        Assert.True(dto.Age == 0);
    }
    
}