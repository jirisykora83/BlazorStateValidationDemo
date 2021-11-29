using System.Linq.Expressions;
using System.Text;

namespace BlazorStateValidationDemo.Extensions;

public static class ExpressionExtensions
{
	public static string GetPropertyPath(this Expression expr)
	{
		var path = new StringBuilder();
		var memberExpression = GetMemberExpression(expr);
		do
		{
			if (path.Length > 0)
			{
				path.Insert(0, ".");
			}

			path.Insert(0, memberExpression.Member.Name);
			memberExpression = GetMemberExpression(memberExpression.Expression);
		}
		while (memberExpression != null);

		return path.ToString();
	}

	public static MemberExpression? GetMemberExpression(this Expression expression)
	{
		if (expression is MemberExpression memberExpression)
		{
			return memberExpression;
		}

		if (expression is LambdaExpression lambdaExpression)
		{
			switch (lambdaExpression.Body)
			{
				case MemberExpression body:
					return body;
				case UnaryExpression unaryExpression:
					return ((MemberExpression)unaryExpression.Operand);
			}
		}

		return null;
	}
}
