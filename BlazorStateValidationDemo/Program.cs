using System.Reflection;
using BlazorState;
using BlazorStateValidationDemo;
using BlazorStateValidationDemo.Behaviors;
using BlazorStateValidationDemo.Features.Booking;
using BlazorStateValidationDemo.Features.Booking.Validations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped(typeof(IPipelineBehavior<,>),
	typeof(ValidationBehavior<,>));

// !!! Currently "AddBlazorState" do not added store which are derived from other class..
builder.Services.AddScoped<BookingState>();

// validators // TODO: register via reflection...
builder.Services.AddScoped<AbstractValidator<BookingState>, BookingStateValidator>();

builder.Services.AddBlazorState(
	options =>
	{
		options.Assemblies =
			new[]
			{
				typeof(Program).GetTypeInfo().Assembly,
			};

		// TODO: this currently do not work (if state have abstract parent..?)
		options.UseCloneStateBehavior = false;
	}
);

await builder.Build().RunAsync();
