using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;
using SmartAnalyzers.CSharpExtensions.Annotations;

// ReSharper disable once CheckNamespace
namespace BlazorStateValidationDemo.Features.Booking;

public partial class BookingState
{
	public class SetTestDataAction : IAction
	{
		[InitRequired]
		public TestDate Data { get; init; }
	}

	public class SetTestDataActionHandler : ActionHandler<SetTestDataAction>
	{
		private BookingState BookingState => Store.GetState<BookingState>();

		public SetTestDataActionHandler(IStore store)
			: base(store)
		{
		}

		public override async Task<Unit> Handle(SetTestDataAction action,
			CancellationToken cancellationToken)
		{
			BookingState.SetTestData(action.Data);
			return default;
		}
	}
}