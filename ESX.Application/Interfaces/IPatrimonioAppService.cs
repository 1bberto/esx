using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESX.Application.Interfaces
{
    public interface IPatrimonioAppService : IAppServiceBase<Patrimonio, IPatrimonioService, IPatrimonioRepository>
    {
        Task<Patrimonio> SaveAsync(Patrimonio patrimonio);
        Task UpdateAsync(object objId, Patrimonio patrimonio);
        Task<IList<Patrimonio>> ObterPatrimoniosMarcaAsync(int marcaId);
    }
}