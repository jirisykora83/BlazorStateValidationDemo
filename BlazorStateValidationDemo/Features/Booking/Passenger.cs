namespace BlazorStateValidationDemo.Features.Booking;

public record Passenger
{
	public string Firstname { get; set; }

	public string Lastname { get; set; }

	public string Email { get; set; }

	//... other properties (birth date, id number, id expiration etc..)
}