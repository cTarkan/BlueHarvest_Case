using BH.Case.Infrastructure;
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

			services.AddInfrastructureServices(configuration);

			return services;
		}
	}
}
