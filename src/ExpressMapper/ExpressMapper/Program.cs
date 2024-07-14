using ExpressMapperCore.Configuration;
using ExpressMapperCore.Mapper;

namespace ExpressMapper;
public static class  Program
{
    #region Users

    public class UserDto
    {
        public required int Age { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required Guid Id { get; init; }
        public override string ToString()
        {
            return $"Info: {Name} {Description} {Id}";
        }
    }
    public class User
    {
        public string Name{ get; init; }
        public string Description { get; init; }
        
        public required int Age { get; init; }
        public Guid PhoneId { get; set; }

        public User(string name,string description)
        {
            Name = name;
            Description = description;
        }
        
        public string Address { get; init; }
        
        public override string ToString()
        {
            return $"Info: Name {Name} Description {Description} Adress {Address} PhoneId {PhoneId} Age {Age}";
        }
    }

    public class UserDtoConfig : MapperConfigBase<UserDto,User>
    {
        public override void Configure(IMappingConfigurer<UserDto, User> configurer)
        {
            configurer
                .Map(us => us.PhoneId,dto => dto.Id)
                .IgnoreSource(dto => dto.Name)
                .WithConstructor<string, string>()
                .TwoWaysBuilding();
        }
    }
    public class FullConfigurere : CompositeConfig
    {
        public override void Configure()
        {
            NewConfiguration<User, UserDto>()
                .Map(dto => dto.Id, Guid.NewGuid())
                .Map(dto => dto.Name, "Lox")
                .IgnoreSource(s => s.Description);
        }
    }

    #endregion
    static void Main(string[] args)
    {
        //ConfigManager.ApplyConfig<UserDtoConfig>();
        ConfigManager.ApplyConfig<FullConfigurere>();
        var mapper = Mapper.CreateMapper();
        User dto = new User("me","das")
        {
            Age = 45,
            Description = "Some desc",
            PhoneId = Guid.NewGuid(),
            Name = "Niki"
        };
        
        UserDto auto = mapper.Map<User,UserDto>(dto);
        Console.WriteLine(auto);
    }
    
}