using ExpressMapperCore.Configuration;
using ExpressMapperCore.Mapper;

namespace ExpressMapper;
file class MappingConfig : MapperConfig<User, UserDto>
{
    public override void Configure(IMappingConfigurer<User, UserDto> configurer)
    {
        configurer
            .IgnoreDest(dto => dto.IsActive)
            .IgnoreSource(us => us.Password)
            .Map(dto => dto.Email, "some@gmail.com")
            .Map(dto => dto.GlobalID, us => us.ID)
            .WithConstructor<DateTime>()
            //добавление обратоного маппера (работает только для методов Map и Ignore
            .TwoWaysBuilding();
    }
}
// возможность сразу в одном файле конфигурировать несколько мапперов
// file class Composite : CompositeConfig
// {
//     public override void Configure()
//     {
//         // NewConfiguration<User,UserDto>()
//         //     .IgnoreDest(dto => dto.IsActive)
//         //     .IgnoreSource(us => us.Password)
//         //     .Map(dto => dto.Email, "some@gmail.com")
//         //     .Map(dto => dto.GlobalID, us => us.ID)
//         //     .WithConstructor<DateTime>()
//         //     .TwoWaysBuilding();
//         
//         
//         //NewConfiguration<SomeSourceType,SomeDestType>()
//         //     .Map(...)
//     }
// }
public static class  Program
{
    static void Main(string[] args)
    {
        //применение глобально конфигурации
        ConfigManager.ApplyConfig<MappingConfig>();

        var mapper = Mapper.CreateMapper();
        var user = new User
        {
            //автоматически найдут по имени
            UserName = "auto name",
            Description = "auto desc",
            //игнорируется из конфига
            Password = "ignore password",
            IsActive = true,
            //маппится в соответсвии с конфигом
            ID = Guid.NewGuid(),
            // будут переданы к конструктор
            WasBorn = DateTime.Now,
            //не замапится т.к у UserDto данное поле закрыто для записи
            Age = 32,
            //не замапится т.к у UserDto данное поле имеет другой тип
            Gender = "male"
        };

        var dto = mapper.Map<User, UserDto>(user);
        Console.WriteLine(dto);
        var mappedUser = mapper.Map<UserDto, User>(dto);
        Console.WriteLine(mappedUser);
    }
    
}