using System.Collections.Generic;
using System.Threading.Tasks;
using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Generic;

namespace ESX.Domain.Core.Interfaces.Repository
{
    public interface IPatrimonioRepository : IRepositoryBase<Patrimonio>, IUpdate<Patrimonio>, ISave<Patrimonio>
    {
        Task<IList<Patrimonio>> ObterPatrimoniosPorMarcaAsync(int marcaId);
    }
}