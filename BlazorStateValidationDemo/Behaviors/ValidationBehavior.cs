using BlazorState;
using MediatR;

namespace BlazorStateValidationDemo.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
	private static readonly Type RequestType = typeof(TRequest);
	private static readonly Type ValidatableStateType = typeof(IValidatableState);
	private readonly IStore store;
	private readonly IServiceProvider serviceProvider;

	public ValidationBehavior(IStore store, IServiceProvider serviceProvider)
	{
		this.store = store;
		this.serviceProvider = serviceProvider;
	}

	public async Task<TResponse> Handle(TRequest request,
		CancellationToken cancellationToken,
		RequestHandlerDelegate<TResponse> next)
	{
		var newState = await next().ConfigureAwait(false);
		if (RequestType.DeclaringType is not null &&
			RequestType.DeclaringType.IsAssignableTo(ValidatableStateType))
		{
			var state = (IValidatableState)store.GetState(RequestType.DeclaringType);
			state.Validate(serviceProvider);
		}

		return newState;
	}
}