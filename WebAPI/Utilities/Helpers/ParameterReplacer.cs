using System.Linq.Expressions;

namespace WebAPI.Utilities.Helpers;

public class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return base.VisitParameter(_parameter);
    }

    public ParameterReplacer(ParameterExpression parameter)
    {
        _parameter = parameter;
    }
}