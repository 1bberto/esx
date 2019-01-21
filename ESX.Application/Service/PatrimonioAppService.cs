using ESX.Application.Interfaces;
using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESX.Application.Service
{
    public class PatrimonioAppService :
        AppServiceBase<Patrimonio, IPatrimonioService, IPatrimonioRepository>,
        IPatrimonioAppService
    {
        public PatrimonioAppService(IPatrimonioService service) : base(service)
        { }

        public async Task<IList<Patrimonio>> ObterPatrimoniosMarcaAsync(int marcaId)
        {
            return await GetService().ObterPatrimoniosMarcaAsync(marcaId);
        }

        public async Task<Patrimonio> SaveAsync(Patrimonio patrimonio)
        {
            return await GetService().SaveAsync(patrimonio);
        }

        public async Task UpdateAsync(object objId, Patrimonio patrimonio)
        {
            await GetService().UpdateAsync(objId, patrimonio);
        }
    }
}