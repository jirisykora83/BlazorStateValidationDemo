using FluentValidation;

namespace BlazorStateValidationDemo.Features.Booking.Validations;

public class BookingStateValidator : AbstractValidator<BookingState>
{
	public BookingStateValidator()
	{
		RuleFor(x => x.Passengers)
			.NotEmpty();

		RuleForEach(x => x.Passengers)
			.SetValidator(new PassengerValidator());

		RuleFor(x => x.Data.Email)
			.NotEmpty()
			.EmailAddress();
	}
}

public class PassengerValidator : AbstractValidator<Passenger>
{
	public PassengerValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();

		RuleFor(x => x.Firstname)
			.NotEmpty();

		RuleFor(x => x.Lastname)
			.NotEmpty();
	}
}