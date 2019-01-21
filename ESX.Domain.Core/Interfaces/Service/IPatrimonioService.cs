using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESX.Domain.Core.Interfaces.Service
{
    public interface IPatrimonioService : IServiceBase<Patrimonio, IPatrimonioRepository>
    {
        Task<Patrimonio> SaveAsync(Patrimonio patrimonio);
        Task UpdateAsync(object objId, Patrimonio patrimonio);
        Task<IList<Patrimonio>> ObterPatrimoniosMarcaAsync(int marcaId);
    }
}