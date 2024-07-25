namespace ExpressMapperTest.TestEnitties;

public class Address
{
    public string City { get; set; }
    public string Country { get; set; }
    public string Street { get; set; }
    
}

public class AddressDto
{
    public string City { get; private set; }
    public string Country { get; private set; }
    public string Street { get; private set; }

    public AddressDto(string city,string country,string street)
    {
        City = city;
        Country = country;
        Street = street;
    }
}