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

