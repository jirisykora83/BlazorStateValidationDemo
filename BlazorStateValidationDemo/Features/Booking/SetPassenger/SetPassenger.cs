using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;
using SmartAnalyzers.CSharpExtensions.Annotations;

// ReSharper disable once CheckNamespace
namespace BlazorStateValidationDemo.Features.Booking;

public partial class BookingState
{
	public class SetPassengerAction : IAction
	{
		[InitRequired]
		public int Index { get; init; }

		[InitRequired]
		public Passenger? Passenger { get; init; }
	}

	public class SetPassengerActionHandler : ActionHandler<SetPassengerAction>
	{
		private BookingState BookingState => Store.GetState<BookingState>();

		public SetPassengerActionHandler(IStore store)
			: base(store)
		{
		}

		public override async Task<Unit> Handle(SetPassengerAction action,
			CancellationToken cancellationToken)
		{
			if (action.Passenger is not null)
			{
				BookingState.SetPassengers(action.Index, action.Passenger);
			}

			return default;
		}
	}
}