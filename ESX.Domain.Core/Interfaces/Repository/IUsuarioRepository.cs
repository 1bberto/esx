using ESX.Domain.Core.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESX.Domain.Core.Generic;

namespace ESX.Domain.Core.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>, ISave<Usuario>
    {
        Task<Usuario> LoginAsync(Usuario usuario);
        Task<IList<Role>> UsuarioRolesAsync(string usuarioId);
        Task AdicionarRoleUsuarioAsync(Usuario usuario, Role role);
        Task<bool> VerificarLoginAsync(string login);
    }
}