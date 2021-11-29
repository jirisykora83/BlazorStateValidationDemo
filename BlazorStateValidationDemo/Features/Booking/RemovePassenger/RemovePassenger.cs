using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;
using SmartAnalyzers.CSharpExtensions.Annotations;

// ReSharper disable once CheckNamespace
namespace BlazorStateValidationDemo.Features.Booking;

public partial class BookingState
{
	public class RemovePassengerAction : IAction
	{
		[InitRequired]
		public int Index { get; init; }
	}

	public class RemovePassengerActionHandler : ActionHandler<RemovePassengerAction>
	{
		private BookingState BookingState => Store.GetState<BookingState>();

		public RemovePassengerActionHandler(IStore store)
			: base(store)
		{
		}

		public override async Task<Unit> Handle(RemovePassengerAction action,
			CancellationToken cancellationToken)
		{
			BookingState.RemovePassengers(action.Index);
			return default;
		}
	}
}