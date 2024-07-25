
# ExpressMapper

ExpressMapper is a lightweight and easy-to-use object-to-object mapping library for .NET, designed to streamline the process of transforming data between various layers of your application. It offers customizable mapping configurations, property ignoring, constructor mapping, and composite configurations to handle complex scenarios with ease.

## Features

- **Customizable Mapping Configurations**: Define mappings between source and destination properties with fine-grained control.
- **Ignore Specific Properties**: Choose to ignore properties on either the source or destination side during the mapping.
- **Constructor Mapping**: Use specific constructors when instantiating destination objects, useful for complex objects.
- **Composite Configurations**: Manage multiple mappings for different types within a single configuration class.

## Installation

Install ExpressMapper via NuGet:

```sh
Install-Package ExpressMapper
```

## Getting Started

To use ExpressMapper, start by defining your source and destination classes, and then configure the mappings using the provided configuration classes.

### Example Classes

```csharp
public class User
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UserDto
{
    public string UserName { get; set; }
    public string AdditionalInfo { get; set; }
}
```

### Customizable Mapping Configurations

Define how properties are mapped between different classes:

```csharp
file class UserConfig : MapperConfig<User, UserDto>
{
    public override void Configure(IMappingConfigurer<User, UserDto> configurer)
    {
        configurer
            .Map(dto => dto.UserName, us => us.Name);
    }
}
```

### Ignoring Properties

Exclude certain properties from the mapping process:

```csharp
file class UserConfig : MapperConfig<User, UserDto>
{
    public override void Configure(IMappingConfigurer<User, UserDto> configurer)
    {
        configurer
            .Map(dto => dto.UserName, us => us.Name)
            .IgnoreDest(dto => dto.UserName)
            .IgnoreSource(us => us.Description);
    }
}
```

### Constructor Mapping

Specify the use of constructors with specific parameters:

```csharp
configurer.WithConstructor<string, string>();
```

### Composite Configurations

Combine multiple configurations into a single configuration class:

```csharp
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
```

### Example Usage

```csharp
public static class Program
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
```

This example demonstrates how to create a mapper with combined configurations and map a `User` object to a `UserDto`.

## Conclusion

ExpressMapper provides a flexible and powerful solution for object-to-object mapping in .NET applications. Customize your mappings to fit your specific needs, whether it's for simple DTOs or complex objects.

For more information, please refer to the official documentation or check out the source code examples.
