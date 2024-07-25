using ExpressMapperCore.Configuration;
using ExpressMapperCore.Expressions;
using ExpressMapperCore.Lambdas;
using ExpressMapperCore.Lambdas.LambdaBuilder;
using ExpressMapperCore.MapBuilder;

namespace ExpressMapperCore;

public interface ILibraryFactory
{
    ILambdaCache CreateLambdaCache();
    IConfig<TSource, TDest> CreateConfig<TSource, TDest>(IEnumerable<IClause<TSource, TDest>> clauses);
    IExpressionBuilder CreateExpressionBuilder();
    IConfigurationBuilder<TSource,TDest> CreateConfigBuilder<TSource,TDest>();
    IConfigManager CreateConfigManager(IEnumerable<IConfigProvider> providers);
    ILambdaManager CreateLambdaManager(IConfigManager manager);
    ILambdaBuilder CreateLambdaBuilder();
    IMapExpressionBuilder CreateMapExpressionBuilder();
    IMappingTracker<TSource, TDest> CreateMappingTracker<TSource, TDest>(IConfig<TSource,TDest>? config);
}
public class LibraryFactory : ILibraryFactory
{
    public static ILibraryFactory Instance { get; set; }

    static LibraryFactory()
    {
        Instance = new LibraryFactory();
    }

    public ILambdaCache CreateLambdaCache()
    {
        return new LambdaCache();
    }

    public  virtual IConfig<TSource, TDest> CreateConfig<TSource, TDest>
        (IEnumerable<IClause<TSource, TDest>> clauses)
    {
        return new Config<TSource, TDest>(clauses);
    }

    public virtual IExpressionBuilder CreateExpressionBuilder()
    {
        return new ExpressionBuilder();
    }

    public virtual IConfigurationBuilder<TSource,TDest> CreateConfigBuilder<TSource,TDest>()
    {
        return new ConfigBuilder<TSource, TDest>();
    }
    
    public virtual IConfigManager CreateConfigManager(IEnumerable<IConfigProvider> providers)
    {
        return ConfigManager.CreateManager(providers);
    }

    public virtual ILambdaManager CreateLambdaManager(IConfigManager configManager)
    {
        return new LambdaManager
            (
                CreateLambdaCache(), 
                CreateLambdaBuilder(),
                configManager
            );
    }

    public virtual ILambdaBuilder CreateLambdaBuilder()
    {
        return new LambdaBuilder(CreateMapExpressionBuilder());
    }

    public virtual IMapExpressionBuilder CreateMapExpressionBuilder()
    {
        return new MapExpressionBuilder(CreateExpressionBuilder());
    }

    public virtual IMappingTracker<TSource, TDest> CreateMappingTracker<TSource, TDest>
        (IConfig<TSource,TDest>? config)
    {
        return new MappingTracker<TSource, TDest>(config);
    }
}
