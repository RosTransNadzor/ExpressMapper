using ExpressMapperCore.Configuration;
using ExpressMapperCore.MapBuilder;
namespace ExpressMapperCore.Mapper;

public class Mapper : IMapper
{
    private readonly ILambdaManager _lambdaManager;
    protected Mapper(ILambdaManager lambdaManager)
    {
        _lambdaManager = lambdaManager;
    }

    public static IMapper Create()
    {
        return new Mapper(LibraryFactory.Instance.CreateLambdaManager
            (LibraryFactory.Instance.CreateConfigManager([])));
    }
    public static IMapper<T1> Create<T1>()
        where T1 : IConfigProvider, new()
    {
        return new Mapper<T1>(LibraryFactory.Instance.CreateConfigManager([new T1()]));
    }

    public static IMapper<T1> Create<T1>(T1 provider1)
        where T1 : IConfigProvider
    {
        return new Mapper<T1>(LibraryFactory.Instance.CreateConfigManager([provider1]));
    }
    public static IMapper<T1, T2> Create<T1, T2>()
    where T1 : IConfigProvider, new()
    where T2 : IConfigProvider, new()
    {
        return new Mapper<T1, T2>(LibraryFactory.Instance.CreateConfigManager([ new T1(), new T2() ]));
    }

    public static IMapper<T1, T2> Create<T1, T2>(T1 provider1, T2 provider2)
        where T1 : IConfigProvider
        where T2 : IConfigProvider
    {
        return new Mapper<T1, T2>(LibraryFactory.Instance.CreateConfigManager([ provider1, provider2 ]));
    }

    public static IMapper<T1, T2, T3> Create<T1, T2, T3>()
        where T1 : IConfigProvider, new()
        where T2 : IConfigProvider, new()
        where T3 : IConfigProvider, new()
    {
        return new Mapper<T1, T2, T3>
            (LibraryFactory.Instance.CreateConfigManager([ new T1(), new T2(), new T3() ]));
    }

    public static IMapper<T1, T2, T3> Create<T1, T2, T3>(T1 provider1, T2 provider2, T3 provider3)
        where T1 : IConfigProvider
        where T2 : IConfigProvider
        where T3 : IConfigProvider
    {
        return new Mapper<T1, T2, T3>
            (LibraryFactory.Instance.CreateConfigManager([provider1, provider2, provider3 ]));
    }

    public static IMapper<T1, T2, T3, T4> Create<T1, T2, T3, T4>()
        where T1 : IConfigProvider, new()
        where T2 : IConfigProvider, new()
        where T3 : IConfigProvider, new()
        where T4 : IConfigProvider, new()
    {
        return new Mapper<T1, T2, T3, T4>(LibraryFactory.Instance.
            CreateConfigManager([new T1(), new T2(), new T3(), new T4() ]));
    }

    public static IMapper<T1, T2, T3, T4> Create<T1, T2, T3, T4>
        (T1 provider1, T2 provider2, T3 provider3, T4 provider4)
        where T1 : IConfigProvider
        where T2 : IConfigProvider
        where T3 : IConfigProvider
        where T4 : IConfigProvider
    {
        return new Mapper<T1, T2, T3, T4>(LibraryFactory.Instance.
            CreateConfigManager([ provider1, provider2, provider3, provider4 ]));
    }

    public static IMapper<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>()
        where T1 : IConfigProvider, new()
        where T2 : IConfigProvider, new()
        where T3 : IConfigProvider, new()
        where T4 : IConfigProvider, new()
        where T5 : IConfigProvider, new()
    {
        return new Mapper<T1, T2, T3, T4, T5>(LibraryFactory.Instance
            .CreateConfigManager([ new T1(), new T2(), new T3(), new T4(), new T5() ]));
    }

    public static IMapper<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>
        (T1 provider1, T2 provider2, T3 provider3, T4 provider4, T5 provider5)
        where T1 : IConfigProvider
        where T2 : IConfigProvider
        where T3 : IConfigProvider
        where T4 : IConfigProvider
        where T5 : IConfigProvider
    {
        return new Mapper<T1, T2, T3, T4, T5>
            (LibraryFactory.Instance.CreateConfigManager(
                [provider1, provider2, provider3, provider4, provider5 ]
                
            ));
    }

    public static IMapper<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>()
        where T1 : IConfigProvider, new()
        where T2 : IConfigProvider, new()
        where T3 : IConfigProvider, new()
        where T4 : IConfigProvider, new()
        where T5 : IConfigProvider, new()
        where T6 : IConfigProvider, new()
    {
        return new Mapper<T1, T2, T3, T4, T5, T6>(LibraryFactory.Instance.CreateConfigManager
            ([ new T1(), new T2(), new T3(), new T4(), new T5(), new T6() ]
                
            ));
    }

    public static IMapper<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>
        (T1 provider1, T2 provider2, T3 provider3, T4 provider4, T5 provider5, T6 provider6)
        where T1 : IConfigProvider
        where T2 : IConfigProvider
        where T3 : IConfigProvider
        where T4 : IConfigProvider
        where T5 : IConfigProvider
        where T6 : IConfigProvider
    {
        return new Mapper<T1, T2, T3, T4, T5, T6>(
            LibraryFactory.Instance.CreateConfigManager(
                [ provider1, provider2, provider3, provider4, provider5, provider6]
            ));
    }

    public static IMapper<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>()
        where T1 : IConfigProvider, new()
        where T2 : IConfigProvider, new()
        where T3 : IConfigProvider, new()
        where T4 : IConfigProvider, new()
        where T5 : IConfigProvider, new()
        where T6 : IConfigProvider, new()
        where T7 : IConfigProvider, new()
    {
        return new Mapper<T1, T2, T3, T4, T5, T6, T7>
            (LibraryFactory.Instance.CreateConfigManager(
                [ new T1(), new T2(), new T3(), new T4(), new T5(), new T6(), new T7() ]
                
            ));
    }

    public static IMapper<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>
        (T1 provider1, T2 provider2, T3 provider3, T4 provider4, T5 provider5, T6 provider6, T7 provider7)
        where T1 : IConfigProvider
        where T2 : IConfigProvider
        where T3 : IConfigProvider
        where T4 : IConfigProvider
        where T5 : IConfigProvider
        where T6 : IConfigProvider
        where T7 : IConfigProvider
    {
        return new Mapper<T1, T2, T3, T4, T5, T6, T7>(LibraryFactory.Instance
            .CreateConfigManager(
                [ provider1, provider2, provider3, provider4, provider5, provider6, provider7 ]
            ));
    }

    public static IMapper<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>()
        where T1 : IConfigProvider, new()
        where T2 : IConfigProvider, new()
        where T3 : IConfigProvider, new()
        where T4 : IConfigProvider, new()
        where T5 : IConfigProvider, new()
        where T6 : IConfigProvider, new()
        where T7 : IConfigProvider, new()
        where T8 : IConfigProvider, new()
    {
        return new Mapper<T1, T2, T3, T4, T5, T6, T7, T8>(LibraryFactory.Instance
            .CreateConfigManager(
                [ new T1(), new T2(), new T3(), new T4(), new T5(), new T6(), new T7(), new T8()]
            ));
    }

    public static IMapper<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>
        (
            T1 provider1, T2 provider2, T3 provider3, T4 provider4, 
            T5 provider5, T6 provider6, T7 provider7, T8 provider8
        )
        where T1 : IConfigProvider
        where T2 : IConfigProvider
        where T3 : IConfigProvider
        where T4 : IConfigProvider
        where T5 : IConfigProvider
        where T6 : IConfigProvider
        where T7 : IConfigProvider
        where T8 : IConfigProvider
    {
        return new Mapper<T1, T2, T3, T4, T5, T6, T7, T8>
            (LibraryFactory.Instance.CreateConfigManager(
                [provider1, provider2, provider3, provider4, provider5, provider6, provider7, provider8 ]
            ));
    }

    public static IMapper<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        where T1 : IConfigProvider, new()
        where T2 : IConfigProvider, new()
        where T3 : IConfigProvider, new()
        where T4 : IConfigProvider, new()
        where T5 : IConfigProvider, new()
        where T6 : IConfigProvider, new()
        where T7 : IConfigProvider, new()
        where T8 : IConfigProvider, new()
        where T9 : IConfigProvider, new()
    {
        return new Mapper<T1, T2, T3, T4, T5, T6, T7, T8, T9>(LibraryFactory.Instance
            .CreateConfigManager(
                [new T1(), new T2(), new T3(), new T4(), new T5(), new T6(), new T7(), new T8(), new T9() ]
            ));
    }

    public static IMapper<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>
        (
            T1 provider1, T2 provider2, T3 provider3, T4 provider4, T5 provider5,
            T6 provider6, T7 provider7, T8 provider8, T9 provider9
        )
        where T1 : IConfigProvider
        where T2 : IConfigProvider
        where T3 : IConfigProvider
        where T4 : IConfigProvider
        where T5 : IConfigProvider
        where T6 : IConfigProvider
        where T7 : IConfigProvider
        where T8 : IConfigProvider
        where T9 : IConfigProvider
    {
        return new Mapper<T1, T2, T3, T4, T5, T6, T7, T8, T9>(LibraryFactory.Instance
            .CreateConfigManager(
                [ 
                    provider1, provider2, provider3, provider4, provider5, 
                    provider6, provider7, provider8, provider9
                ]
            ));
    }

    public static IMapper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> 
        Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        where T1 : IConfigProvider, new()
        where T2 : IConfigProvider, new()
        where T3 : IConfigProvider, new()
        where T4 : IConfigProvider, new()
        where T5 : IConfigProvider, new()
        where T6 : IConfigProvider, new()
        where T7 : IConfigProvider, new()
        where T8 : IConfigProvider, new()
        where T9 : IConfigProvider, new()
        where T10 : IConfigProvider, new()
    {
        return new Mapper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
            (LibraryFactory.Instance.CreateConfigManager([
                new T1(), new T2(), new T3(), new T4(), new T5(), 
                new T6(), new T7(), new T8(), new T9(), new T10() 
            ]));
    }

    public static IMapper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> 
        Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
        (
            T1 provider1, T2 provider2, T3 provider3, T4 provider4, T5 provider5,
            T6 provider6, T7 provider7, T8 provider8, T9 provider9, T10 provider10
            
        )
        where T1 : IConfigProvider
        where T2 : IConfigProvider
        where T3 : IConfigProvider
        where T4 : IConfigProvider
        where T5 : IConfigProvider
        where T6 : IConfigProvider
        where T7 : IConfigProvider
        where T8 : IConfigProvider
        where T9 : IConfigProvider
        where T10 : IConfigProvider
    {
        return new Mapper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
            (LibraryFactory.Instance.CreateConfigManager
            ([
                provider1, provider2, provider3, provider4, provider5, 
                provider6, provider7, provider8, provider9, provider10 
            ]));
    }
    public TDest Map<TSource, TDest>(TSource source)
    {
        Func<TSource,TDest> func = _lambdaManager.GetLambda<TSource, TDest>();
        return func(source);
    }
}

public class Mapper<T> : Mapper, IMapper<T>
    where T : IConfigProvider
{

    public Mapper(IConfigManager configManager) 
        : base(LibraryFactory.Instance.CreateLambdaManager(configManager)
        )
    {}
}
public class Mapper<T1,T2> : Mapper, IMapper<T1,T2>
    where T1 : IConfigProvider
    where T2 : IConfigProvider
{

    public Mapper(IConfigManager configManager) 
        : base(LibraryFactory.Instance.CreateLambdaManager(configManager)
        )
    {}
}
public class Mapper<T1,T2,T3> : Mapper, IMapper<T1,T2,T3>
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
{

    public Mapper(IConfigManager configManager) 
        : base(LibraryFactory.Instance.CreateLambdaManager(configManager)
        )
    {}
}
public class Mapper<T1,T2,T3,T4> : Mapper, IMapper<T1,T2,T3,T4>
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
{

    public Mapper(IConfigManager configManager) 
        : base(LibraryFactory.Instance.CreateLambdaManager(configManager)
        )
    {}
}

public class Mapper<T1, T2, T3, T4, T5> : Mapper, IMapper<T1, T2, T3, T4, T5>
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider
{

    public Mapper(IConfigManager configManager) 
        : base(LibraryFactory.Instance.CreateLambdaManager(configManager)
        )
    {}
}
public class Mapper<T1, T2, T3, T4, T5,T6> : Mapper, IMapper<T1, T2, T3, T4, T5,T6>
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider
    where T6 : IConfigProvider
{

    public Mapper(IConfigManager configManager) 
        : base(LibraryFactory.Instance.CreateLambdaManager(configManager)
        )
    {}
}
public class Mapper<T1, T2, T3, T4, T5,T6,T7> : Mapper, IMapper<T1, T2, T3, T4, T5,T6,T7>
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider
    where T6 : IConfigProvider
    where T7 : IConfigProvider
{

    public Mapper(IConfigManager configManager) 
        : base(LibraryFactory.Instance.CreateLambdaManager(configManager)
        )
    {}
}
public class Mapper<T1, T2, T3, T4, T5,T6,T7,T8> : Mapper, IMapper<T1, T2, T3, T4, T5,T6,T7,T8>
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider
    where T6 : IConfigProvider
    where T7 : IConfigProvider
    where T8 : IConfigProvider
{

    public Mapper(IConfigManager configManager) 
        : base(LibraryFactory.Instance.CreateLambdaManager(configManager)
        )
    {}
}
public class Mapper<T1, T2, T3, T4, T5,T6,T7,T8,T9> : Mapper, IMapper<T1, T2, T3, T4, T5,T6,T7,T8,T9>
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider
    where T6 : IConfigProvider
    where T7 : IConfigProvider
    where T8 : IConfigProvider
    where T9 : IConfigProvider
{

    public Mapper(IConfigManager configManager) 
        : base(LibraryFactory.Instance.CreateLambdaManager(configManager)
        )
    {}
}
public class Mapper<T1, T2, T3, T4, T5,T6,T7,T8,T9,T10> : Mapper, IMapper<T1, T2, T3, T4, T5,T6,T7,T8,T9,T10>
    where T1 : IConfigProvider
    where T2 : IConfigProvider
    where T3 : IConfigProvider
    where T4 : IConfigProvider
    where T5 : IConfigProvider
    where T6 : IConfigProvider
    where T7 : IConfigProvider
    where T8 : IConfigProvider
    where T9 : IConfigProvider
    where T10 : IConfigProvider
{

    public Mapper(IConfigManager configManager) 
        : base(LibraryFactory.Instance.CreateLambdaManager(configManager)
        )
    {}
}