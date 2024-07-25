using ExpressMapperCore.Expressions;

namespace ExpressMapperCore.Configuration.Config.Clause;

public static class ClauseHelper
{
   public static IEnumerable<MappingMember> GetDestIgnoreMembers<TSource,TDest>
      (IConfig<TSource,TDest>? config)
   {
      return config?.Clauses.OfType<IIgnoreClause<TSource, TDest>>()
                .Where(ic => ic is
                {
                   IsValidClause: true, 
                   DestinationIgnoreMember: not null
                })
                .Select(ic => ic.DestinationIgnoreMember!) 
             ?? Enumerable.Empty<MappingMember>();
   }
   public static IEnumerable<MappingMember> GetSourceIgnoreMembers<TSource,TDest>
      (IConfig<TSource,TDest>? config)
   {
      return config?.Clauses.OfType<IIgnoreClause<TSource, TDest>>()
                .Where(ic => ic is
                {
                   IsValidClause: true, 
                   SourceIgnoreMember: not null
                })
                .Select(ic => ic.SourceIgnoreMember!) 
             ?? Enumerable.Empty<MappingMember>();
   }

   public static IEnumerable<IMapClause<TSource, TDest>> GetMapClauses<TSource, TDest>
      (IConfig<TSource, TDest>? config)
   {
      return config?.Clauses.OfType<IMapClause<TSource, TDest>>()
                .Where(mc => mc.IsValidClause)
             ?? Enumerable.Empty<IMapClause<TSource, TDest>>();
   }

   public static IConstructClause<TSource,TDest>? GetConstructorClause<TSource,TDest>
      (IConfig<TSource,TDest>? config)
   {
      return config?.Clauses.OfType<IConstructClause<TSource, TDest>>()
         .FirstOrDefault(cc => cc.IsValidClause);
   }
}