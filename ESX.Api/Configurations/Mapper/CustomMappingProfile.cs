using AutoMapper;
using ESX.Api.Models.ViewModel;
using ESX.Domain.Core.Entity;

namespace ESX.Api.Configurations.Mapper
{
    public class CustomMappingProfile : Profile
    {
        public CustomMappingProfile()
        {
            CreateMap<UsuarioLoginViewModel, Usuario>().AfterMap((s, d) =>
            {
                d.Login = s.Login;
                d.Senha = s.Senha;
            });

            CreateMap<RoleViewModel, Role>().AfterMap((s, d) => { d.RoleId = s.RoleId; });
            //CreateMap<UsuarioViewModel, Usuario>().AfterMap(
            //    (s, d) =>
            //    {
            //        d.Login = s.Login;
            //        d.Senha = s.Senha;
            //        d.Nome = s.Nome;
            //    });
        }
    }
}