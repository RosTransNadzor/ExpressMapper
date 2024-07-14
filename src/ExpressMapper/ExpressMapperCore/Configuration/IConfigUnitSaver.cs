namespace ExpressMapperCore.Configuration;

public interface IConfigUnitSaver
{
    public void AddUnitsToStorage(IMapStorage<IConfigUnit> storage);
}