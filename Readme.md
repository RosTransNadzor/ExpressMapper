
# ExpressMapper Usage Guide

ExpressMapper is a lightweight, fast, and easy-to-use .NET object-to-object mapper. This guide provides an overview of its features and configuration options.

## Features

- **Single Mapping Configuration** (`MapperConfigBase<TSource, TDest>`)
- **Composite Configuration for Multiple Mappings** (`CompositeConfig`)
- **Custom Mapping Logic** (`Map()`)
- **Ignoring Fields** (`IgnoreSource`, `IgnoreDest`)
- **Constructor Selection** (`WithConstructor`)
- **Bidirectional Mapping** (`TwoWaysBuilding`)

## Configuration Examples

### 1. Single Mapping Configuration

Define a mapping configuration between two types using `MapperConfigBase`.

```csharp
public class EmployeeDto
{
    public required int Id { get; init; }
    public required string FullName { get; init; }
    public required string Department { get; init; }
    public override string ToString()
    {
        return $"Employee: {FullName} from {Department} with ID {Id}";
    }
}

public class Employee
{
    public string Name { get; init; }
    public string Dept { get; init; }
    public required int EmployeeId { get; init; }

    public Employee(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Department: {Dept}, ID: {EmployeeId}";
    }
}

public class EmployeeDtoConfig : MapperConfigBase<EmployeeDto, Employee>
{
    public override void Configure(IMappingConfigurer<EmployeeDto, Employee> configurer)
    {
        configurer
            .Map(emp => emp.EmployeeId, dto => dto.Id)
            .IgnoreSource(dto => dto.Department)
            .WithConstructor<string>()
            .TwoWaysBuilding();
    }
}
```

### 2. Composite Configuration for Multiple Mappings

Configure multiple mappings within a single configuration using `CompositeConfig`.

```csharp
public class ProjectDto
{
    public required int ProjectId { get; init; }
    public required string ProjectName { get; init; }
}

public class Project
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public class FullConfiguration : CompositeConfig
{
    public override void Configure()
    {
        NewConfiguration<Employee, EmployeeDto>()
            .Map(dto => dto.Id, Guid.NewGuid())
            .Map(dto => dto.FullName, "John Doe")
            .IgnoreSource(s => s.Name);

        NewConfiguration<Project, ProjectDto>()
            .Map(dto => dto.ProjectId, p => p.Id)
            .Map(dto => dto.ProjectName, p => p.Name);
    }
}
```

### 3. Custom Mapping Logic with Map()

Specify custom mappings directly.

```csharp
var mapper = Mapper.CreateMapper();

Employee emp = new Employee("John")
{
    EmployeeId = 123,
    Dept = "Engineering"
};

EmployeeDto empDto = mapper.Map<Employee, EmployeeDto>(emp);
Console.WriteLine(empDto);
```

### 4. Ignoring Fields

Ignore fields in the source or destination during mapping.

```csharp
configurer
    .IgnoreSource(dto => dto.Department)
    .IgnoreDest(emp => emp.Dept);
```

### 5. Constructor Selection

Select a constructor and match parameters by name.

```csharp
configurer.WithConstructor<string>();
```

### 6. Bidirectional Mapping

Enable two-way mapping between types.

```csharp
configurer.TwoWaysBuilding();
```

## Example Usage

```csharp
public class Program
{
    static void Main(string[] args)
    {
        ConfigManager.ApplyConfig<FullConfiguration>();
        var mapper = Mapper.CreateMapper();

        Employee emp = new Employee("Jane")
        {
            EmployeeId = 456,
            Dept = "HR"
        };

        EmployeeDto empDto = mapper.Map<Employee, EmployeeDto>(emp);
        Console.WriteLine(empDto);

        Project proj = new Project
        {
            Id = 789,
            Name = "Project X"
        };

        ProjectDto projDto = mapper.Map<Project, ProjectDto>(proj);
        Console.WriteLine(projDto);
    }
}
```

With these configurations, ExpressMapper allows for flexible and customizable object-to-object mappings, supporting various complex scenarios and requirements.
