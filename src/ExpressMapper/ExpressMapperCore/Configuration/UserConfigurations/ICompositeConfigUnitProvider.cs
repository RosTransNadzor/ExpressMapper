namespace ExpressMapperCore.Configuration;

public interface ICompositeMapperConfig : IConfigUnitSaver
{
    void Configure();
}

public abstract class  CompositeConfig : IConfigUnitSaver
{
    private readonly ICollection<IConfigBuilder> _configBuilders = [];

    public abstract void Configure();

    public IMappingConfigurer<TSource,TDest> NewConfiguration<TSource, TDest>()
    {
        GenericConfigBuilder<TSource,TDest> builder = new GenericConfigBuilder<TSource,TDest>();
        _configBuilders.Add(builder);
        return builder;
    }
    void IConfigUnitSaver.AddUnitsToStorage(IMapStorage<IConfigUnit> storage)
    {
        Configure();
        foreach (var configBuilder in _configBuilders)
        {
            ConfigUnitHelper.AddUnitToStorage(storage,configBuilder.BuildConfing());
        }
    }
}