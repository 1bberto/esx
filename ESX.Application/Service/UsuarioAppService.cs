using ESX.Application.Interfaces;
using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;
using System.Threading.Tasks;

namespace ESX.Application.Service
{
    public class UsuarioAppService :
        AppServiceBase<Usuario, IUsuarioService, IUsuarioRepository>,
        IUsuarioAppService
    {
        public UsuarioAppService(IUsuarioService service) : base(service)
        { }

        public async Task<Usuario> ValidarUsuarioAsync(Usuario usuario)
        {
            return await GetService().ValidarUsuarioAsync(usuario);
        }

        public async Task AdicionarRoleUsuarioAsync(string usuarioId, Role role)
        {
            await GetService().AdicionarRoleUsuarioAsync(usuarioId,role);
        }

        public async Task<string> AdicionarUsuarioAsync(Usuario usuario)
        {
            return await GetService().AdicionarUsuarioAsync(usuario);
        }
    }
}