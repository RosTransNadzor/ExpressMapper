namespace ExpressMapper;

public class Product
{
    public Guid ProductId { get; set; }
}

public class ProductDto
{
    public Guid Id { get; set; }
}

public class Address
{
    
}

public class AddressDto
{
    public string City { get; set; }   
}