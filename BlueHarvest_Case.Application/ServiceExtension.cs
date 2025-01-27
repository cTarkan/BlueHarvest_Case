using BlueHarvest_Case.Application.Interfaces;
using BlueHarvest_Case.Application.Services;
using BlueHarvest_Case.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BlueHarvest_Case.Application
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddInfrastructureServices(configuration);

			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<ITransactionService, TransactionService>();
			services.AddScoped<IUserAccountService, UserAccountService>();

			return services;
		}
	}
}
