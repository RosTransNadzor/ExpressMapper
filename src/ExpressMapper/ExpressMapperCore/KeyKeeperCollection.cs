using System.Collections.Concurrent;
namespace ExpressMapperCore;

public interface IMapCache<T>
    where T : IKeyKeeper
{
    T GetOrAdd(MapKey key, Func<T> faultFunc);
    void Clear();
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
    private readonly ConcurrentDictionary<MapKey,T> _storage;

    public KeyKeeperCollection()
    {
        _storage = new ConcurrentDictionary<MapKey,T>();
    }

    private void AddOrUpdate(T value)
    {
        _storage.AddOrUpdate(value.Key,value,(_,_) => value);
    }
    
    public T GetOrAdd
        (MapKey key, Func<T> faultFunc)
    {
        if (_storage.ContainsKey(key))
            return _storage[key];

        T value = faultFunc();
        AddOrUpdate(value);
        return value;
    }

    public T? Get(MapKey key)
    {
        if (!_storage.ContainsKey(key))
            return default;

        return _storage[key];
    }
    public void Add(T value)
    {
        AddOrUpdate(value);
    }

    public void Clear()
    {
        _storage.Clear();
    }
}