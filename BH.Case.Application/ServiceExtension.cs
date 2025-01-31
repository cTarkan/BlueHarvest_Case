using BH.Case.Application.Behaviors;
using BH.Case.Application.Validators.Commands;
using BH.Case.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BH.Case.Application
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			// MediatR for CQRS and Handlers
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

			// Register Validators and auto validation
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
			services.AddValidatorsFromAssemblyContaining<CreateAccountCommandValidator>();

			services.AddInfrastructureServices(configuration);

			return services;
		}
	}
}
