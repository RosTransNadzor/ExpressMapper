using ExpressMapperCore.Configuration;
using ExpressMapperCore.Mapper;
using ExpressMapperTest.TestEnitties;

namespace ExpressMapperTest;

file class ProductMapConfig : MapperConfig<Product, ProductDto>
{
    public override void Configure(IMappingConfigurer<Product, ProductDto> configurer)
    {
        configurer
            .Map(dto => dto.Id, _ => Guid.NewGuid())
            .Map(dto => dto.Name, product => product.ProductName);
    }
}
public class MapTest
{
    [Fact]
    public void CorrectMappingValue()
    {
        var mapper = Mapper.Create<ProductMapConfig>();
        var product = new Product
        {
            ProductName = "name"
        };
        var dto = mapper.Map<Product,ProductDto>(product);
        Assert.False(dto.Id == new Guid());
    }

    [Fact]
    public void CorrectMappingSourceMember()
    {
        var mapper = Mapper.Create<ProductMapConfig>();
        var product = new Product
        {
            ProductName = "name"
        };
        var dto = mapper.Map<Product,ProductDto>(product);
        Assert.True(dto.Name == product.ProductName);
    }
}