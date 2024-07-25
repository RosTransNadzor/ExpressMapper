namespace ExpressMapperCore.Configuration;

public interface IConfigProvider
{
    public IEnumerable<IConfig> GetConfigUnits();
}