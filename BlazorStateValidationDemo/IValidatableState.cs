namespace BlazorStateValidationDemo;

public interface IValidatableState
{
	public void Validate(IServiceProvider services);
}