namespace ExpressMapperCore.Configuration;

public interface IMapperConfig<TSource,TDest> : IConfigUnitSaver
{
    void Configure(IMappingConfigurer<TSource,TDest> configurer);
}

public abstract class MapperConfigBase<TSource, TDest> : IMapperConfig<TSource, TDest>
{
    private readonly IConfigBuilder<TSource,TDest> _mappingConfigurer = 
        new GenericConfigBuilder<TSource, TDest>();
    
    public abstract void Configure(IMappingConfigurer<TSource,TDest> configurer);

    void IConfigUnitSaver.AddUnitsToStorage(IMapStorage<IConfigUnit> storage)
    {
        Configure(_mappingConfigurer);
        ConfigUnitHelper.AddUnitToStorage(storage, _mappingConfigurer.BuildConfig());
    }

}