namespace BlazorStateValidationDemo.Features.Booking;

public partial class BookingState : ValidatableState<BookingState>
{
	private readonly List<Passenger> passengers = new()
	{
		new(),
		new(),
	};

	// test enumerable field
	public IReadOnlyList<Passenger> Passengers => passengers.AsReadOnly();

	// test complex field
	public TestDate Data { get; private set; } = new();

	// test simple filed
	public string SimpleField { get; private set; }

	public void AddPassengers()
	{
		passengers.Add(new());
	}

	public void RemovePassengers(int index)
	{
		passengers.RemoveAt(index);
	}

	public void SetPassengers(int index, Passenger passenger)
	{
		passengers[index] = passenger;
	}

	public void SetTestData(TestDate data)
	{
		Data = data;
	}

	public override void Initialize()
	{
	}
}

// Some nested data related to booking
public record TestDate
{
	public string Email { get; set; }
}