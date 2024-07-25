
namespace ExpressMapperCore.Exceptions;

public class CannotFindConstructorWithoutParamsException : Exception
{
    public readonly Type SearchedType;
    public CannotFindConstructorWithoutParamsException(Type searchedType)
    {
        SearchedType = searchedType;
    }

    public CannotFindConstructorWithoutParamsException(string message, Type searchedType) : base(message)
    {
        SearchedType = searchedType;
    }

    public CannotFindConstructorWithoutParamsException(string message, Exception inner, Type searchedType) : base(message, inner)
    {
        SearchedType = searchedType;
    }
}