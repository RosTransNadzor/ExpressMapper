namespace ExpressMapperCore.Configuration;

public abstract class MapperConfig<TSource, TDest> : IConfigProvider
{
    private readonly IConfigurationBuilder<TSource, TDest> _buidler =
        LibraryFactory.Instance.CreateConfigBuilder<TSource, TDest>();
    
    public abstract void Configure(IMappingConfigurer<TSource,TDest> configurer);

    IEnumerable<IConfig> IConfigProvider.GetConfigUnits()
    {
        Configure(_buidler);
        yield return _buidler.Config;
        if (_buidler.ReverseConfig is not null)
            yield return _buidler.ReverseConfig;
    }

}