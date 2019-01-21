using System.Linq;
using AutoMapper;
using ESX.Api.Models.ModelView;
using ESX.Api.Models.ViewModel;
using ESX.Domain.Core.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESX.Api.Configurations.Mapper
{
    public class AutoMapperConfiguration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                CreateMap(mc);

                mc.AddProfile<CustomMappingProfile>();
                mc.AddProfile<DomainToModelViewProfile>();
            });

            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }

        private static void CreateMap(IMapperConfigurationExpression mc)
        {
            var vms = typeof(IViewModel).Assembly.GetTypes()?.ToList()
                .Where(vm => typeof(IViewModel).IsAssignableFrom(vm)).ToList();

            var mvs = typeof(IModelView).Assembly.GetTypes()?.ToList()
                .Where(vm => typeof(IModelView).IsAssignableFrom(vm)).ToList();

            var entities = typeof(IEntity).Assembly.GetTypes()?.ToList()
                .Where(vm => typeof(IEntity).IsAssignableFrom(vm)).ToList();

            entities.Where(e => vms.Select(v => v.Name).Any(n => n == e.Name + "ViewModel")).ToList().ForEach(e =>
            {
                var vm = vms.First(v => v.Name == e.Name + "ViewModel");
                mc.CreateMap(e, vm);
                mc.CreateMap(vm, e);
            });

            entities.Where(e => mvs.Select(v => v.Name).Any(n => n == e.Name + "ModelView")).ToList().ForEach(e =>
            {
                var mv = mvs.First(v => v.Name == e.Name + "ModelView");
                mc.CreateMap(e, mv);
                mc.CreateMap(mv, e);
            });
        }
    }
}