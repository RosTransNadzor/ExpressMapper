using System.Linq.Expressions;
using ExpressMapperCore.Expressions;

namespace ExpressMapperTest;

public class MemberSearchTest
{
    class User
    {
        //prop
        public required string Name { get; set; }
        //field
        public required string Description;

    }
    private readonly ParameterExpression _param;

    public MemberSearchTest()
    {
        _param = Expression.Parameter(typeof(User));
    }

    [Fact]
    public void CorrectFormProp()
    {
        var expression = MemberSearchHelper.FormMemberAccess(_param,nameof(User.Name), typeof(string));
        Assert.NotNull(expression);
        Assert.Equal(typeof(User).GetProperty(nameof(User.Name)),expression.Member);
    }

    [Fact]
    public void CorrectFormField()
    {
        var expression = MemberSearchHelper.FormMemberAccess
            (_param, nameof(User.Description), typeof(string));

        Assert.NotNull(expression);
        Assert.Equal(typeof(User).GetField(nameof(User.Description)),expression.Member);
    }
    [Fact]
    public void CorrectFindWithIncorrectType()
    {
        var expression = MemberSearchHelper.FormMemberAccess
            (_param, nameof(User.Description), typeof(int));

        Assert.Null(expression);
    }
    [Fact]
    public void CannotFindUnexistingMember()
    {
        var expression = MemberSearchHelper.FormMemberAccess
            (_param, "AbraKadabra", typeof(string));

        Assert.Null(expression);
    }
}