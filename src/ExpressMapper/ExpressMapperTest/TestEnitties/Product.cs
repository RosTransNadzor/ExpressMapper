namespace ExpressMapperTest.TestEnitties;

public class Product
{
    public string ProductName { get; set; }
}

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}