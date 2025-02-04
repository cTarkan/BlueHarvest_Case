using BH.Case.Infrastructure.Data;
using BH.Case.Infrastructure.Interfaces;
using BH.Case.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BH.Case.Infrastructure
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseNpgsql(
				configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<IAccountRepository, AccountRepository>();
			services.AddScoped<ITransactionRepository, TransactionRepository>();
			return services;
		}
	}
}
