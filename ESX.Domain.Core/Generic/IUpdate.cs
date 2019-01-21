using System.Threading.Tasks;

namespace ESX.Domain.Core.Generic
{
    public interface IUpdate<T>
    {
        Task UpdateAsync(object objId, T entity);
    }
}