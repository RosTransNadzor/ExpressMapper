using System.Linq.Expressions;
using ExpressMapperCore.Configuration.Config.Clause;

namespace ExpressMapperCore.Configuration;

public interface IMappingConfigurer<TSource, TDest>
{
    IMappingConfigurer<TSource, TDest> Map<TMember>
        (Expression<Func<TDest, TMember>> destMember, Expression<Func<TSource, TMember>> lambda);

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

public partial class ConfigBuilder<TSource, TDest>
{
    private bool isTwoWaysConfig;

    public IMappingConfigurer<TSource, TDest> Map<TMember>
        (Expression<Func<TDest, TMember>> destMember, Expression<Func<TSource, TMember>> lambda)
    {
        _clauses.Add(new MapClause<TSource,TDest,TMember>(destMember,lambda));
        return this;
    }

    public IMappingConfigurer<TSource, TDest> IgnoreDest<TMember>(Expression<Func<TDest, TMember>> destMember)
    {
        _clauses.Add(new IgnoreClause<TSource, TDest, TMember>(destMember));
        return this;
    }

    public IMappingConfigurer<TSource, TDest> IgnoreSource<TMember>
        (Expression<Func<TSource, TMember>> sourceMember)
    {
        _clauses.Add(new IgnoreClause<TSource,TDest,TMember>(sourceMember));
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
}