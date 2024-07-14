using System.Linq.Expressions;

namespace ExpressMapperCore.Expressions;

public class DestinationWithFullAsigning
{
    private readonly Expression _destVariable;
    private readonly Expression _creationExpression;
    private readonly IEnumerable<Expression> _fullAssigns;

    public DestinationWithFullAsigning
    (
        Expression destVariable, 
        Expression creationExpression,
        IEnumerable<Expression> fullAssigns
    )
    {
        _destVariable = destVariable;
        _creationExpression = creationExpression;
        _fullAssigns = fullAssigns;
    }

    public Expression ToExpression()
    {
        return Expression.Block([(ParameterExpression)_destVariable],GetAllExpressions());
    }

    private IEnumerable<Expression> GetAllExpressions()
    {
        var variableCreation = Expression.Assign(_destVariable, _creationExpression);
        yield return variableCreation;

        foreach (var assign in _fullAssigns)
        {
            yield return assign;
        }

        yield return _destVariable;
    }
}