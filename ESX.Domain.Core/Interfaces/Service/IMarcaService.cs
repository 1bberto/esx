using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using System.Threading.Tasks;

namespace ESX.Domain.Core.Interfaces.Service
{
    public interface IMarcaService : IServiceBase<Marca, IMarcaRepository>
    {
        Task<Marca> SaveAsync(Marca marca);
        Task UpdateAsync(object objId, Marca marca);
    }
}