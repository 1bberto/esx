using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using System.Threading.Tasks;

namespace ESX.Domain.Core.Interfaces.Service
{
    public interface IUsuarioService : IServiceBase<Usuario, IUsuarioRepository>
    {
        Task<Usuario> ValidarUsuarioAsync(Usuario usuario);
        Task AdicionarRoleUsuarioAsync(string usuarioId, Role role);
        Task<string> AdicionarUsuarioAsync(Usuario usuario);
    }
}