using System;
using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Exceptions;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;
using System.Linq;
using System.Threading.Tasks;

namespace ESX.Domain.Core.Service
{
    public class UsuarioService : ServiceBase<Usuario, IUsuarioRepository>, IUsuarioService
    {
        private readonly IRoleRepository _roleRepository;

        public UsuarioService(
            IUsuarioRepository repository,
            IRoleRepository roleRepository) : base(repository)
        {
            _roleRepository = roleRepository;
        }

        public async Task AdicionarRoleUsuarioAsync(string usuarioId, Role role)
        {
            if (!Guid.TryParse(usuarioId, out Guid usu))
                throw new DomainException("Codigo de usuario invalido!");

            var usuario = await GetRepository().GetByIdAsync(usuarioId);

            if (usuario == null)
                throw new DomainException("Usuario nao encontrado!");

            var itemRole = await _roleRepository.GetByIdAsync(role.RoleId);

            if (itemRole == null)
                throw new DomainException("Role nao encontrada!");

            var roles = await GetRepository().UsuarioRolesAsync(usuarioId);

            if (roles.Any(x => x.RoleId == role.RoleId))
                throw new DomainException("Role ja cadastrada para usuario");

            await GetRepository().AdicionarRoleUsuarioAsync(usuario, role);
        }

        public async Task<string> AdicionarUsuarioAsync(Usuario usuario)
        {
            var usuarioExiste = await GetRepository().VerificarLoginAsync(usuario.Login);

            if (usuarioExiste)
                throw new DomainException("Login ja cadastrado!");

            var usuarioNovo = await GetRepository().SaveAsync(usuario);

            return usuarioNovo.UsuarioId;
        }

        public async Task<Usuario> ValidarUsuarioAsync(Usuario usuario)
        {
            var usu = await GetRepository().LoginAsync(usuario);

            if (usu == null) return null;

            usu.Roles = await GetRepository().UsuarioRolesAsync(usu.UsuarioId);

            return usu;
        }
    }
}