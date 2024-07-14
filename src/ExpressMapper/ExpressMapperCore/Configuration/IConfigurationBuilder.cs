using System.Linq.Expressions;
using ExpressMapperCore.Configuration.Clause;

namespace ExpressMapperCore.Configuration;

public interface IMappingConfigurer<TSource, TDest>
{
    IMappingConfigurer<TSource, TDest> Map<TMember>
        (Expression<Func<TDest, TMember>> destMember,Expression<Func<TSource, TMember>> sourceMember);

    IMappingConfigurer<TSource, TDest> Map<TMember>
        (Expression<Func<TDest, TMember>> destMember, TMember value);

    IMappingConfigurer<TSource, TDest> IgnoreDest<TMember>(Expression<Func<TDest, TMember>> destMember);
    IMappingConfigurer<TSource, TDest> IgnoreSource<TMember>(Expression<Func<TSource, TMember>> sourceMember);
    void TwoWaysBuilding();

    IMappingConfigurer<TSource, TDest> WithConstructor<T1>();
    IMappingConfigurer<TSource, TDest> WithConstructor<T1, T2>();
    IMappingConfigurer<TSource, TDest> WithConstructor<T1, T2, T3>();

    IMappingConfigurer<TSource, TDest> WithConstructor<T1, T2, T3, T4>();

    IMappingConfigurer<TSource, TDest> WithConstructor<T1, T2, T3, T4, T5>();
    IMappingConfigurer<TSource, TDest> WithConstructor<T1, T2, T3, T4, T5, T6>();
    IMappingConfigurer<TSource, TDest> WithConstructor<T1, T2, T3, T4, T5, T6, T7>();
}

public interface IConfigBuilder
{
    IConfigUnit BuildConfing();
}
public interface IConfigurationBuilder<TSource,TDest> : IConfigBuilder
{
    ConfigUnit<TSource, TDest> BuildConfig();

    IConfigUnit IConfigBuilder.BuildConfing()
    {
        return BuildConfig();
    }
}

public interface IConfigBuilder<TSourse, TDest>
    : IConfigurationBuilder<TSourse, TDest>, IMappingConfigurer<TSourse, TDest>;
public class GenericConfigBuilder<TSource, TDest> : IConfigBuilder<TSource,TDest>
{
    private readonly ICollection<IClause<TSource,TDest>> _clauses = [];
    private bool isTwoWaysConfig;
    
    public IMappingConfigurer<TSource, TDest> Map<TMember>
        (Expression<Func<TDest, TMember>> destMember, Expression<Func<TSource, TMember>> sourceMember)
    {
        _clauses.Add(new MapWithMemberClause<TSource,TDest,TMember>(sourceMember,destMember));
        return this;
    }

    public IMappingConfigurer<TSource, TDest> Map<TMember>
        (Expression<Func<TDest, TMember>> destMember, TMember value)
    {
        _clauses.Add(new MapWithValueClause<TSource,TDest,TMember>(destMember,value));
        return this;
    }

    public IMappingConfigurer<TSource, TDest> IgnoreDest<TMember>(Expression<Func<TDest, TMember>> destMember)
    {
        _clauses.Add(new IngnoreClause<TSource, TDest, TMember>(destMember));
        return this;
    }

    public IMappingConfigurer<TSource, TDest> IgnoreSource<TMember>
        (Expression<Func<TSource, TMember>> sourceMember)
    {
        _clauses.Add(new IngnoreClause<TSource,TDest,TMember>(sourceMember));
        return this;
    }
    
    public IMappingConfigurer<TSource, TDest> WithConstructor<T1>()
    {
        var info = typeof(TDest).GetConstructor([typeof(T1)]);
        _clauses.Add(new ConstructorClause<TSource, TDest>(info));
        return this;
    }
    public IMappingConfigurer<TSource, TDest> WithConstructor<T1,T2,T3,T4,T5,T6,T7>()
    {
        var info = typeof(TDest).GetConstructor
            ([typeof(T1),typeof(T2),typeof(T3),typeof(T4),typeof(T5),typeof(T6),typeof(T7)]);
        _clauses.Add(new ConstructorClause<TSource, TDest>(info));
        return this;
    }
    public IMappingConfigurer<TSource, TDest> WithConstructor<T1,T2>()
    {
        var info = typeof(TDest).GetConstructor([typeof(T1),typeof(T2)]);
        _clauses.Add(new ConstructorClause<TSource, TDest>(info));
        return this;
    }
    public IMappingConfigurer<TSource, TDest> WithConstructor<T1,T2,T3>()
    {
        var info = typeof(TDest).GetConstructor([typeof(T1),typeof(T2),typeof(T3)]);
        _clauses.Add(new ConstructorClause<TSource, TDest>(info));
        return this;
    }
    public IMappingConfigurer<TSource, TDest> WithConstructor<T1,T2,T3,T4>()
    {
        var info = typeof(TDest).GetConstructor
            ([typeof(T1),typeof(T2),typeof(T3),typeof(T4)]);
        _clauses.Add(new ConstructorClause<TSource, TDest>(info));
        return this;
    }
    public IMappingConfigurer<TSource, TDest> WithConstructor<T1,T2,T3,T4,T5>()
    {
        var info = typeof(TDest).GetConstructor
            ([typeof(T1),typeof(T2),typeof(T3),typeof(T4),typeof(T5)]);
        
        _clauses.Add(new ConstructorClause<TSource, TDest>(info));
        return this;
    }
    public IMappingConfigurer<TSource, TDest> WithConstructor<T1,T2,T3,T4,T5,T6>()
    {
        var info = typeof(TDest).GetConstructor
            ([typeof(T1),typeof(T2),typeof(T3),typeof(T4),typeof(T5),typeof(T6)]);
        _clauses.Add(new ConstructorClause<TSource, TDest>(info));
        return this;
    }
    
    public void TwoWaysBuilding()
    {
        isTwoWaysConfig = true;
    }

    public ConfigUnit<TSource, TDest> BuildConfig()
    {
        if (isTwoWaysConfig)
        {
            var reverse = new ConfigUnit<TDest, TSource>(
                _clauses.OfType<IReverseAbleClause<TSource, TDest>>()
                    .Select(c => c.GetReverseClause()).ToList()
            );
            return new ConfigUnit<TSource, TDest>(_clauses, reverse);
        }
        return new ConfigUnit<TSource, TDest>(_clauses);
    }
}