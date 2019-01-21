using ESX.Domain.Core.Generic;
using ESX.Domain.Core.Interfaces.Repository;

namespace ESX.Domain.Core.Interfaces.Service
{
    public interface IServiceBase<T, R> :
        IGet<T>,
        IDelete<T>
        where T : class
        where R : IRepositoryBase<T>
    {
        R GetRepository();
    }
}