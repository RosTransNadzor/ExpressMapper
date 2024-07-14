using System.Linq.Expressions;

namespace ExpressMapperCore.Expressions;

public static class ExpressionHelper
{
    public static Expression DefaultValue(Type type) => Expression.Constant(default, type);
}