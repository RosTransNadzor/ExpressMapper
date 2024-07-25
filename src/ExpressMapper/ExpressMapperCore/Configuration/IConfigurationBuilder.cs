namespace ExpressMapperCore.Configuration;

public interface IConfigurationBuilder<TSource, TDest> 
    : IConfigBuilder, IMappingConfigurer<TSource, TDest>;
public interface IConfigBuilder
{
    public IConfig Config { get; }
    public IConfig? ReverseConfig { get; }
}
public partial class ConfigBuilder<TSource, TDest> : IConfigurationBuilder<TSource,TDest>
{
    private readonly ICollection<IClause<TSource,TDest>> _clauses = [];

    private IConfig<TSource,TDest>? _config;
    private IConfig<TDest, TSource>? _reverseConfig;

    public IConfig Config => _config ??= BuildConfig();

    public IConfig? ReverseConfig
    {
        get
        {
            if (isTwoWaysConfig)
            {
                _reverseConfig ??= BuildReverseConfig();
                return _reverseConfig;
            }

            return null;
        }
    }
    private IConfig<TSource,TDest> BuildConfig()
    {
        return LibraryFactory.Instance.CreateConfig(_clauses);
    }

    private IConfig<TDest, TSource> BuildReverseConfig()
    {
        return LibraryFactory.Instance.CreateConfig(
            _clauses.OfType<IReverseAbleClause<TSource, TDest>>()
                .Select(c => c.GetReverseClause()).ToList()
        );
    }
}