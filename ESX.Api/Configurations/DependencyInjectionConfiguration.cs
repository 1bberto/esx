using System;
using System.Linq;
using ESX.Application.Service;
using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;
using ESX.Domain.Core.Service;
using ESX.Domain.Shared.Interfaces;
using ESX.Infra.Data.Persistence;
using ESX.Infra.Data.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ESX.Api.Configurations
{
    public class DependencyInjectionConfiguration
    {
        public static void Register(IServiceCollection services)
        {
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //AppService
            RegistrarInterfaces(services, typeof(AppServiceBase<Marca, IMarcaService, IMarcaRepository>), "Service", "AppService");
            //Services
            RegistrarInterfaces(services, typeof(ServiceBase<Marca, IMarcaRepository>), "Service", "Service");
            //Repositorios
            RegistrarInterfaces(services, typeof(RepositoryBase<Marca>), "Repository", "Repository");
        }
        private static void RegistrarInterfaces(IServiceCollection services, Type typeBase, string containsInNamespace, string sulfix)
        {
            var types = typeBase
                .Assembly
                .GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace) &&
                               type.Namespace.Contains(containsInNamespace) &&
                               type.Name.EndsWith(sulfix) &&
                               !type.IsGenericType &&
                               type.IsClass &&
                               type.GetInterfaces().Any());

            foreach (var type in types)
            {
                var interfaceType = type
                    .GetInterfaces()?
                    .FirstOrDefault(t => t.Name == $"I{type.Name}");
                if (interfaceType == null)
                {
                    continue;
                }
                services.AddScoped(interfaceType, type);
            }
        }
    }
}