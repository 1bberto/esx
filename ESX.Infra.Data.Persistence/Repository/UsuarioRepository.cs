using System.Collections.Generic;
using Dapper;
using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Shared.Interfaces;
using System.Threading.Tasks;
using ESX.Domain.Core.Generic;

namespace ESX.Infra.Data.Persistence.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IUnitOfWork uow) : base(uow)
        { }

        public async Task AdicionarRoleUsuarioAsync(Usuario usuario, Role role)
        {
            var param = new DynamicParameters();
            param.Add($"@UsuarioId", usuario.UsuarioId);
            param.Add($"@RoleId", role.RoleId);
            await Uow.GetConnection().ExecuteAsync("EXEC USP_UsuarioRole_INS @UsuarioId, @RoleId", param, Uow.GeTransaction());
        }

        public async Task<Usuario> LoginAsync(Usuario usuario)
        {
            var param = new DynamicParameters();
            param.Add($"@Login", usuario.Login);
            param.Add($"@Senha", usuario.Senha);
            var data = await Uow.GetConnection().QueryFirstOrDefaultAsync<Usuario>
                ("EXEC USP_UsuarioLogin @Login, @Senha", param, Uow.GeTransaction());
            return data;
        }

        public async Task<Usuario> SaveAsync(Usuario usuario)
        {
            var param = new DynamicParameters();
            param.Add($"@Login", usuario.Login);
            param.Add($"@Senha", usuario.Senha);
            param.Add($"@Nome", usuario.Nome);
            usuario.UsuarioId = await Uow.GetConnection().QueryFirstOrDefaultAsync<string>
                ("EXEC USP_Usuario_INS @Login, @Senha, @Nome", param, Uow.GeTransaction());
            return usuario;
        }

        public async Task<IList<Role>> UsuarioRolesAsync(string usuarioId)
        {
            var param = new DynamicParameters();
            param.Add($"@UsuarioId", usuarioId);
            var dados = await Uow.GetConnection().QueryAsync<Role>
                ("EXEC USP_UsuarioRole_SEL @UsuarioId", param, Uow.GeTransaction());
            return dados.AsList();
        }

        public async Task<bool> VerificarLoginAsync(string login)
        {
            var param = new DynamicParameters();
            param.Add($"@Login", login);
            var dados = await Uow.GetConnection().QueryFirstOrDefaultAsync<int>
                ("EXEC USP_VerificaUsuarioLogin @Login", param, Uow.GeTransaction());
            return dados > 0;
        }

        Task<Usuario> ISave<Usuario>.SaveAsync(Usuario entity)
        {
            throw new System.NotImplementedException();
        }
    }
}