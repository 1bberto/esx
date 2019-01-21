using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;
using System.Threading.Tasks;

namespace ESX.Application.Interfaces
{
    public interface IMarcaAppService : IAppServiceBase<Marca, IMarcaService, IMarcaRepository>
    {
        Task<Marca> SaveAsync(Marca marca);
        Task UpdateAsync(object objId, Marca marca);
    }
}