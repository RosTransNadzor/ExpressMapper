
using ExpressMapperCore.Configuration;

namespace ExpressMapperCore.Mapper;

/// <summary>
/// Сontract for mapping objects from one type to another.
/// </summary>
public interface IMapper
{
    /// <summary>
    /// Maps an object of type <typeparamref name="TSource"/> to an object of type <typeparamref name="TDest"/>.
    /// </summary>
    /// <typeparam name="TSource">The source type to map from.</typeparam>
    /// <typeparam name="TDest">The destination type to map to.</typeparam>
    /// <returns>An object of type <typeparamref name="TDest"/> mapped from an object of type
    /// <typeparamref name="TSource"/>.</returns>
    TDest Map<TSource, TDest>(TSource source);
}

public interface IMapper<T1> : IMapper
    where T1 : IConfigProvider;

public interface IMapper<T1, T2> : IMapper
    where T1 : IConfigProvider
    where T2 : IConfigProvider;

public interface IMapper<T1, T2, T3> : IMapper
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider;

public interface IMapper<T1, T2, T3, T4> : IMapper
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider;

public interface IMapper<T1, T2, T3, T4, T5> : IMapper
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider;

public interface IMapper<T1, T2, T3, T4, T5, T6> : IMapper
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider
    where T6 : IConfigProvider;

public interface IMapper<T1, T2, T3, T4, T5, T6, T7> : IMapper
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider
    where T6 : IConfigProvider
    where T7 : IConfigProvider;
    
public interface IMapper<T1, T2, T3, T4, T5, T6, T7, T8> : IMapper
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider
    where T6 : IConfigProvider
    where T7 : IConfigProvider
    where T8 : IConfigProvider;

public interface IMapper<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IMapper
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider
    where T6 : IConfigProvider
    where T7 : IConfigProvider
    where T8 : IConfigProvider
    where T9 : IConfigProvider;

public interface IMapper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IMapper
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider
    where T6 : IConfigProvider
    where T7 : IConfigProvider
    where T8 : IConfigProvider
    where T9 : IConfigProvider
    where T10 : IConfigProvider;


