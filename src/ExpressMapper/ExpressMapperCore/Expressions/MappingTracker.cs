using ExpressMapperCore.Configuration;
using ExpressMapperCore.Configuration.Config.Clause;

namespace ExpressMapperCore.Expressions;

public interface IMappingTracker<TSource,TDest>
{
    IEnumerable<IMappingRule> GetMappingRules();
    IMappingTracker<TSource, TDest> RemoveIgnored();
    IMappingTracker<TSource, TDest> MapConstructorParams();
    IMappingTracker<TSource, TDest> AutoMapByName();
    IMappingTracker<TSource, TDest> MapByClauses();
}
public class MappingTracker<TSource,TDest> : IMappingTracker<TSource,TDest>
{
    private readonly ICollection<IMappingRule> _mappingRules = [];
    private IList<MappingMember> _sourceMembers;
    private IList<MappingMember> _destinationMembers;
    private readonly IConfig<TSource,TDest>? _config;

    public MappingTracker(IConfig<TSource, TDest>? config)
    {
        _config = config;
        _sourceMembers = MemberSearchHelper.FindAllMembers<TSource>().ToList();
        _destinationMembers = MemberSearchHelper.FindAllMembers<TDest>(true).ToList();
    }
    public IEnumerable<IMappingRule> GetMappingRules()
    {
        return _mappingRules;
    }

    public IMappingTracker<TSource, TDest> RemoveIgnored()
    {
        var sourceIgnoreMembers = ClauseHelper.GetSourceIgnoreMembers(_config);
        var destIgnoreMembers = ClauseHelper.GetDestIgnoreMembers(_config);
        _sourceMembers = _sourceMembers
            .Where(mm => !sourceIgnoreMembers.Contains(mm)).ToList();
        _destinationMembers = _destinationMembers
            .Where(mm => !destIgnoreMembers.Contains(mm)).ToList();
        return this;
    }

    public IMappingTracker<TSource, TDest> MapConstructorParams()
    {
        var constructorClause = ClauseHelper.GetConstructorClause(_config);
        if (constructorClause is not null)
        {
            var ctorRule = new ConstructroRule
            {
                ConstructorParams = constructorClause.ConstructorParams
                    .Select(p => new Tuple<Type,MappingMember?>(
                        p.Type,
                        _sourceMembers.SingleOrDefault(sm => sm.Name == NameHelper.ToUpperFirstLatter(p.Name) 
                                                             && sm.Type == p.Type)
                    )).ToArray(),
                Info = constructorClause.ConstructorInfo!
                    
            };
            _mappingRules.Add(ctorRule);
        }

        return this;
    }

    public IMappingTracker<TSource, TDest> AutoMapByName()
    {
        var enumerable = _destinationMembers
            .Where(dm => _sourceMembers.Any(sm => sm.Name == dm.Name && sm.Type == dm.Type))
            .Select(dm => new
            {
                SourceMember = _sourceMembers.Single(sm => sm.Name == dm.Name && dm.Type == sm.Type),
                DestinationMember = dm,
            }).ToList();

        foreach (var container in enumerable)
        {
            _sourceMembers.Remove(container.SourceMember);
            _destinationMembers.Remove(container.DestinationMember);
            _mappingRules.Add(new AutoMappingRule
            {
                SourceMember = container.SourceMember,
                DestinationMember = container.DestinationMember
            });
        }

        return this;
    }

    public IMappingTracker<TSource, TDest> MapByClauses()
    {
        var mapClauses = ClauseHelper.GetMapClauses(_config);
        foreach (var mapClause in mapClauses)
        {
            // ! - GetMapClauses() guarantess that clauses are valid
            _destinationMembers.Remove(mapClause.DestinationMember!);
            _mappingRules.Add(new MapClauseRule
            {
                DestinationMember = mapClause.DestinationMember!,
                SourceLambda = mapClause.Expression!
            });
        }

        return this;
    }
}