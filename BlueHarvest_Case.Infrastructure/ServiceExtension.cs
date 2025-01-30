using BlueHarvest_Case.Infrastructure.Interfaces;
using BlueHarvest_Case.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlueHarvest_Case.Infrastructure
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IAccountRepository, AccountRepository>();
			services.AddSingleton<ITransactionRepository, TransactionRepository>();
			services.AddSingleton<IUserRepository, UserRepository>();

			return services;
		}
	}
}
