using Dapper;
using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Shared.Interfaces;
using System.Threading.Tasks;

namespace ESX.Infra.Data.Persistence.Repository
{
    public class MarcaRepository : RepositoryBase<Marca>, IMarcaRepository
    {
        public MarcaRepository(IUnitOfWork uow) : base(uow)
        { }

        public async Task<Marca> SaveAsync(Marca entity)
        {
            var param = new DynamicParameters();
            param.Add($"@Nome", entity.Nome);
            entity.MarcaId = await Uow.GetConnection().QueryFirstOrDefaultAsync<int>(
                $"EXEC {InsertProcedure} @Nome", param, Uow.GetTransaction()
            );
            return entity;
        }

        public async Task UpdateAsync(object objId, Marca entity)
        {
            var param = new DynamicParameters();
            param.Add($"@{GetKeyField()}", objId);
            param.Add($"@Nome", entity.Nome);
            await Uow.GetConnection().ExecuteAsync(
                $"EXEC {UpdateProcedure} @{GetKeyField()}, @Nome", param, Uow.GetTransaction()
            );
        }

        public async Task<Marca> VerificarMarcaNomeAsync(string nome)
        {
            var param = new DynamicParameters();
            param.Add($"@{GetKeyField()}", null);
            param.Add($"@Nome", nome);
            var marca = await Uow.GetConnection().QueryFirstOrDefaultAsync<Marca>(
                $"EXEC {SelectProcedure} @{GetKeyField()}, @Nome", param, Uow.GetTransaction()
            );
            return marca;
        }
    }
}