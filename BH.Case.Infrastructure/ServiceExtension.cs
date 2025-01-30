using BH.Case.Infrastructure.Interfaces;
using BH.Case.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BH.Case.Infrastructure
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
