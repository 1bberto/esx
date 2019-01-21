using System.Threading.Tasks;

namespace ESX.Domain.Core.Generic
{
    public interface ISave<T> where T : class
    {
        Task<T> SaveAsync(T entity);
    }
}