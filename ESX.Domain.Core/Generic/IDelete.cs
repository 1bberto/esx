using System.Threading.Tasks;

namespace ESX.Domain.Core.Generic
{
    public interface IDelete<T>
    {
        Task DeleteAsync(object objId);
    }
}