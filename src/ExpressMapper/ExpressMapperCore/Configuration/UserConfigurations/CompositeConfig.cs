namespace ExpressMapperCore.Configuration.UserConfigurations;
public abstract class  CompositeConfig : IConfigProvider
{
    private readonly ICollection<IConfigBuilder> _configBuilders = [];

    public abstract void Configure();

    public IMappingConfigurer<TSource,TDest> NewConfiguration<TSource, TDest>()
    {
        var builder = LibraryFactory.Instance.CreateConfigBuilder<TSource, TDest>();
        _configBuilders.Add(builder);
        return builder;
    }
    IEnumerable<IConfig> IConfigProvider.GetConfigUnits()
    {
        Configure();
        foreach (var cofigBuilder in _configBuilders)
        {
            yield return cofigBuilder.Config;
            if (cofigBuilder.ReverseConfig is not null) 
                yield return cofigBuilder.ReverseConfig;
        }
    }
}