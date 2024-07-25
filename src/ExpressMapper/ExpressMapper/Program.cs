using ExpressMapper;
using ExpressMapperCore.Configuration;
using ExpressMapperCore.Configuration.UserConfigurations;
using ExpressMapperCore.Mapper;

file class UserConfig : MapperConfig<User, UserDto>
{
    public override void Configure(IMappingConfigurer<User, UserDto> configurer)
    {
        configurer
            .Map(dto => dto.UserName, us => us.Name)
            .IgnoreDest(dto => dto.UserName)
            .IgnoreSource(us => us.Description)
            .WithConstructor<string, string>();
    }
}

file class GeneralConfig : CompositeConfig
{
    public override void Configure()
    {
        NewConfiguration<Address, AddressDto>()
            .IgnoreDest(dto => dto.City);

        NewConfiguration<Product, ProductDto>()
            .Map(dto => dto.Id, product => product.ProductId);
    }
}
public static class  Program
{
    static void Main(string[] args)
    {
        var mapper = Mapper.Create<GeneralConfig, UserConfig>();
        var user = new User
        {
            Description = "desc"
        };
        var dto = mapper.Map<User, UserDto>(user);
        Console.WriteLine(dto);
    }
}