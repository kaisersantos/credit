using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Credit.Infra.Adapter.EfCore.Config
{
    public static class EfCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddEfCoreAdapter(
            this IServiceCollection services,
            EfCoreAdapterOptions efCoreAdapterOptions)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (efCoreAdapterOptions == null)
                throw new ArgumentNullException(nameof(efCoreAdapterOptions));

            services.AddDbContext<CreditDbContext>(options =>
                options.UseSqlServer(efCoreAdapterOptions.ConnectionString));

            return services;
        }
    }
}
