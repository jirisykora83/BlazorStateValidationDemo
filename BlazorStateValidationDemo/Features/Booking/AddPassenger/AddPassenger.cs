using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;

// ReSharper disable once CheckNamespace
namespace BlazorStateValidationDemo.Features.Booking;

public partial class BookingState
{
	public class AddPassengerAction : IAction
	{
	}

	public class AddPassengerActionHandler : ActionHandler<AddPassengerAction>
	{
		private BookingState BookingState => Store.GetState<BookingState>();

		public AddPassengerActionHandler(IStore store)
			: base(store)
		{
		}

		public override async Task<Unit> Handle(AddPassengerAction action,
			CancellationToken cancellationToken)
		{
			BookingState.AddPassengers();

			return default;
		}
	}
}