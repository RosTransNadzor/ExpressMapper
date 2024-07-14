namespace ExpressMapperCore.Configuration;

public static class ConfigManager
{
    private static readonly IMapStorage<IConfigUnit> _configsStorage 
        = new KeyKeeperCollection<IConfigUnit>();
    
    public static void ApplyConfig<T>()
        where T : class, IConfigUnitSaver, new()
    {
        T config = new T();
        config.AddUnitsToStorage(_configsStorage);
    }

    public static void ApplyConfig(IConfigUnitSaver configUnitSaver)
    {
        configUnitSaver.AddUnitsToStorage(_configsStorage);
    }

    public static void ApplyConfig(params IConfigUnitSaver[] configs)
    {
        foreach (var config in configs)
        {
            ApplyConfig(config);
        }
    }

    public static IConfigUnit<TSource,TDest>? GetConfigUnit<TSource, TDest>()
    {
        return _configsStorage.Get(MapKey.Form<TSource, TDest>()) as IConfigUnit<TSource, TDest>;
    }
}