using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESX.Domain.Core.Generic
{
    public interface IGet<T> where T : class
    {
        Task<T> GetByIdAsync(object objId);
        Task<IList<T>> GetAllAsync();
    }
}