using Credit.Core.Application.UseCases.Clientes.Create;
using Credit.Core.Application.UseCases.Clientes.Edit;
using Credit.Core.Application.UseCases.Clientes.Remove;
using Credit.Core.Application.UseCases.Financiamentos.Create;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Credit.Core.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        private static IServiceCollection? _services;

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));

            _services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            ValidatorOptions.Global.LanguageManager.Enabled = false;

            _services.AddAutoMapper(Assembly.GetExecutingAssembly());

            AddClienteServices();
            AddFinanciamentoServices();
            AddParcelaServices();

            return _services;
        }

        private static void AddClienteServices()
        {
            _services?.AddScoped<ICreateClienteUseCase, CreateClienteUseCase>();
            _services?.AddScoped<IEditClienteUseCase, EditClienteUseCase>();
            _services?.AddScoped<IRemoveClienteUseCase, RemoveClienteUseCase>();
        }

        private static void AddFinanciamentoServices()
        {
            _services?.AddScoped<ICreateFinanciamentoUseCase, CreateFinanciamentoUseCase>();
        }

        private static void AddParcelaServices()
        {

        }
    }
}