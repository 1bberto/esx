using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Generic;
using System.Threading.Tasks;

namespace ESX.Domain.Core.Interfaces.Repository
{
    public interface IMarcaRepository : IRepositoryBase<Marca>, IUpdate<Marca>, ISave<Marca>
    {
        Task<Marca> VerificarMarcaNomeAsync(string nome);
    }
}