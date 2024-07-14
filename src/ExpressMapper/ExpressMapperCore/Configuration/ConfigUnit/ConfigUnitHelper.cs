namespace ExpressMapperCore.Configuration;

public class ConfigUnitHelper
{
    public static void AddUnitToStorage(IMapStorage<IConfigUnit> storage,IConfigUnit configUnit)
    {
        storage.Add(configUnit);
        if(configUnit.ReverseWayUnit is not null)
            storage.Add(configUnit.ReverseWayUnit);
    }
}