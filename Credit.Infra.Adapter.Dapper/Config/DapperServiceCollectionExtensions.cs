using Microsoft.Extensions.DependencyInjection;

namespace Credit.Infra.Adapter.Dapper.Config
{
    public static class DapperServiceCollectionExtensions
    {
        public static IServiceCollection AddDapperAdapter(
            this IServiceCollection services,
            DapperAdapterOptions dapperAdapterOptions)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (dapperAdapterOptions == null)
                throw new ArgumentNullException(nameof(dapperAdapterOptions));

            services.AddSingleton(new CreditDbContext(dapperAdapterOptions.ConnectionString));

            return services;
        }
    }
}
