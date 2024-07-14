using System.Collections.Concurrent;
namespace ExpressMapperCore;

public interface IMapCache<T>
    where T : IKeyKeeper
{
    T GetOrAdd(MapKey key, Func<T> faultFunc);
}

public interface IMapStorage<T>
    where T : IKeyKeeper
{
    T? Get(MapKey key);
    void Add(T value);
}
public class KeyKeeperCollection<T> : IMapCache<T>,IMapStorage<T>
    where T : IKeyKeeper
{
    private readonly ConcurrentDictionary<MapKey,T> _cache;

    public KeyKeeperCollection()
    {
        _cache = new ConcurrentDictionary<MapKey,T>();
    }

    private void AddOrUpdate(T value)
    {
        _cache.AddOrUpdate(value.Key,value,(_,_) => value);
    }
    
    public T GetOrAdd
        (MapKey key, Func<T> faultFunc)
    {
        if (_cache.ContainsKey(key))
            return _cache[key];

        T value = faultFunc();
        AddOrUpdate(value);
        return value;
    }

    public T? Get(MapKey key)
    {
        if (!_cache.ContainsKey(key))
            return default;

        return _cache[key];
    }
    public void Add(T value)
    {
        AddOrUpdate(value);
    }
}