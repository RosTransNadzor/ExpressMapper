using System.Linq.Expressions;
using System.Reflection;

namespace ExpressMapperCore.Expressions;

public static class MemberSearchHelper
{
    public static MemberExpression? FormMemberAccess(ParameterExpression source, string memberName,Type memberType)
    {
        var member = FindMember(source.Type, memberName, memberType,false);

        if (member is null)
            return default;

        return Expression.PropertyOrField(source, memberName);
    }

    public static MemberForMapping? FindMember(Type source,string memberName,Type memberType,bool isWriteable)
    {
        return TryFindField(source, memberName,memberType,isWriteable)
            ?? TryFindProperty(source, memberName,memberType,isWriteable);
    }

    public static IEnumerable<MemberForMapping> FindAllMembers(Type type, bool isWriteable = false)
    {
        foreach (var prop in type.GetProperties())
        {
            if (isWriteable && prop.CanWrite)
            {
                yield return MemberFromProp(prop);
            }
            else if(!isWriteable)
            {
                yield return MemberFromProp(prop);
            }
        }
        foreach (var field in type.GetFields())
        {
            if (isWriteable && !field.IsInitOnly)
            {
                yield return MemberFromField(field);
            }
            else if(!isWriteable)
            {
                yield return MemberFromField(field);
            }
        }
    }
    private static MemberForMapping? TryFindField
        (Type source, string memberName,Type memberType,bool isWriteable)
    {
        var field = source.GetField(memberName);

        return field is not null && field.FieldType == memberType && (!isWriteable || !field.IsInitOnly)
            ? MemberFromField(field)
            : default;
    }

    private static MemberForMapping? TryFindProperty
        (Type source, string memberName, Type memberType,bool isWriteable)
    {
        var prop = source.GetProperty(memberName);
        return prop is not null && prop.PropertyType == memberType && (!isWriteable || prop.CanWrite)
            ? MemberFromProp(prop)
            : default;
    }

    private static MemberForMapping MemberFromField(FieldInfo fieldInfo)
    {
        return new MemberForMapping
        {
            MemberInfo = fieldInfo,
            MemberName = fieldInfo.Name,
            MemberType = fieldInfo.FieldType
        };
    }
    private static MemberForMapping MemberFromProp(PropertyInfo propertyInfo)
    {
        return new MemberForMapping
        {
            MemberInfo = propertyInfo,
            MemberName = propertyInfo.Name,
            MemberType = propertyInfo.PropertyType
        };
    }
}