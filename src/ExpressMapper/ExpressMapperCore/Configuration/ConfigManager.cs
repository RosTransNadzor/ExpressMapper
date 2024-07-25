using System.Collections.Immutable;

namespace ExpressMapperCore.Configuration;
public interface IConfigManager
{
    IConfig<TSource, TDest>? GetConfig<TSource, TDest>();
}
public class ConfigManager : IConfigManager
{
    private readonly ImmutableDictionary<MapKey, IConfig> _configs;

    private ConfigManager(ImmutableDictionary<MapKey, IConfig> configs)
    {
        _configs = configs;
    }

    public IConfig<TSource, TDest>? GetConfig<TSource, TDest>()
    {
        _configs.TryGetValue(MapKey.Form<TSource, TDest>(), out IConfig? config);
        return config as IConfig<TSource, TDest>;
    }

    public static IConfigManager CreateManager(IEnumerable<IConfigProvider> providers)
    {
        var pairs = providers
            .SelectMany(provider => provider.GetConfigUnits())
            .Select(config => new KeyValuePair<MapKey, IConfig>(config.Key, config));

        return new ConfigManager(ImmutableDictionary.CreateRange(pairs));
    }
    
}