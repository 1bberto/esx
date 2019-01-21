using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;
using System.Threading.Tasks;

namespace ESX.Application.Interfaces
{
    public interface IUsuarioAppService : IAppServiceBase<Usuario, IUsuarioService, IUsuarioRepository>
    {
        Task<Usuario> ValidarUsuarioAsync(Usuario usuario);
        Task AdicionarRoleUsuarioAsync(string usuarioId, Role role);
        Task<string> AdicionarUsuarioAsync(Usuario usuario);
    }
}