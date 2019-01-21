using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Shared.Interfaces;

namespace ESX.Infra.Data.Persistence.Repository
{
    public class PatrimonioRepository : RepositoryBase<Patrimonio>, IPatrimonioRepository
    {
        public PatrimonioRepository(IUnitOfWork uow) : base(uow)
        { }

        public async Task<IList<Patrimonio>> ObterPatrimoniosPorMarcaAsync(int marcaId)
        {
            var param = new DynamicParameters();
            param.Add($"@MarcaId", marcaId);
            var dados = await Uow.GetConnection().QueryAsync<Patrimonio>
                ("EXEC USP_Patrimonio_SEL @MarcaId", param, Uow.GeTransaction());
            return dados.AsList();
        }

        public async Task<Patrimonio> SaveAsync(Patrimonio entity)
        {
            var param = new DynamicParameters();
            param.Add($"@MarcaId", entity.MarcaId);
            param.Add($"@Descricao", entity.Descricao);
            var patrimonio = await Uow.GetConnection().QueryFirstOrDefaultAsync<Patrimonio>(
                $"EXEC {InsertProcedure} @MarcaId, @Descricao", param, Uow.GeTransaction()
            );
            return patrimonio;
        }

        public async Task UpdateAsync(object objId, Patrimonio entity)
        {
            var param = new DynamicParameters();
            param.Add($"@{GetKeyField()}", objId);
            param.Add($"@MarcaId", entity.MarcaId);
            param.Add($"@Descricao", entity.Descricao);
            await Uow.GetConnection().ExecuteAsync(
                $"EXEC {UpdateProcedure} @{GetKeyField()}, @MarcaId, @Descricao", param, Uow.GeTransaction()
            );
        }
    }
}