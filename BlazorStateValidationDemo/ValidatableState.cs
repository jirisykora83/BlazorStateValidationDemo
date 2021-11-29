using System.Linq.Expressions;
using System.Text.Json;
using BlazorState;
using BlazorStateValidationDemo.Extensions;
using FluentValidation;
using FluentValidation.Results;

namespace BlazorStateValidationDemo;

public abstract class ValidatableState<T> : State<T>, IValidatableState
{
	protected ValidationResult? ValidationResult;

	public ValidationResult? GetValidationResult()
	{
		return ValidationResult;
	}

	public ValidationResult? GetValidation<TProperty>(
		Expression<Func<T, TProperty>> expression)
	{
		if (ValidationResult is null)
		{
			return null;
		}

		string propertyPath = expression.GetPropertyPath();

		var result = new ValidationResult();
		foreach (var error in ValidationResult.Errors)
		{
			if (error.PropertyName.Equals(propertyPath))
			{
				result.Errors.Add(error);
			}
		}

		return result;
	}

	public ValidationResult? GetValidation<TProperty, TTest>(
		Expression<Func<T, IEnumerable<TProperty>>> expression,
		Expression<Func<TProperty, TTest>> properties,
		int index)
	{
		if (ValidationResult is null)
		{
			return null;
		}

		string propertyPath = expression.GetPropertyPath();
		string nestedPath = properties.GetPropertyPath();
		string path = $"{propertyPath}[{index}].{nestedPath}";

		var result = new ValidationResult();
		foreach (var error in ValidationResult.Errors)
		{
			if (error.PropertyName.Equals(path))
			{
				result.Errors.Add(error);
			}
		}

		return result;
	}

	// TODO: IEnumerable with type which have IEnumerable property?

	public void Validate(IServiceProvider services)
	{
		var validator = services.GetService<AbstractValidator<T>>();
		if (validator is not null &&
			this is T state)
		{
			ValidationResult = validator.Validate(state);
			Console.WriteLine($"ValidationResult: {JsonSerializer.Serialize(ValidationResult)}");
		}
	}
}