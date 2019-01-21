using System.Collections.Generic;
using System.Threading.Tasks;
using ESX.Application.Interfaces;
using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;

namespace ESX.Application.Service
{
    public class MarcaAppService :
        AppServiceBase<Marca, IMarcaService, IMarcaRepository>,
        IMarcaAppService
    {
        public MarcaAppService(IMarcaService service) : base(service)
        { }

        public async Task<Marca> SaveAsync(Marca marca)
        {
            return await GetService().SaveAsync(marca);
        }

        public async Task UpdateAsync(object objId, Marca marca)
        {
            await GetService().UpdateAsync(objId, marca);
        }
    }
}