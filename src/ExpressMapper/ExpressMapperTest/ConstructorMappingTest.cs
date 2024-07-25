using ExpressMapperCore.Configuration;
using ExpressMapperCore.Exceptions;
using ExpressMapperCore.Mapper;
using ExpressMapperTest.TestEnitties;

namespace ExpressMapperTest;

file class MappingWithConstructor : MapperConfig<Address,AddressDto>
{
    public override void Configure(IMappingConfigurer<Address, AddressDto> configurer)
    {
        configurer
            .WithConstructor<string, string, string>();
    }
}

file class MappingWithIncorrectConstructorParams : MapperConfig<Address, AddressDto>
{
    public override void Configure(IMappingConfigurer<Address, AddressDto> configurer)
    {
        // non-existing constructor
        configurer.WithConstructor<string, int, DateTime, string>();
    }
}

public class ConstructorMappingTest
{
    [Fact]
    public void MappingConstructorParamsToUpper()
    {
        var mapper = Mapper.Create<MappingWithConstructor>();
        var address = new Address
        {
            City = "city",
            Country = "country",
            Street = "street"
        };
        var addressDto = mapper.Map<Address, AddressDto>(address);
        Assert.True(address.City == addressDto.City);
        Assert.True(address.Country == addressDto.Country);
        Assert.True(address.Street == addressDto.Street);
    }

    [Fact]
    public void MappingNonExistingConstructor()
    {
        var mapper = Mapper.Create<MappingWithIncorrectConstructorParams>();
        var address = new Address
        {
            City = "city",
            Country = "country",
            Street = "street"
        };
        Assert.Throws<CannotFindConstructorWithoutParamsException>
            (() => mapper.Map<Address, AddressDto>(address));
    }
}