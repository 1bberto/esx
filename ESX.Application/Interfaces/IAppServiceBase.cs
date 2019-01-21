using ESX.Domain.Core.Generic;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;

namespace ESX.Application.Interfaces
{
    public interface IAppServiceBase<T, S, R> :
        IGet<T>,
        IDelete<T>
        where T : class
        where S : IServiceBase<T, R>
        where R : IRepositoryBase<T>
    {
        S GetService();
    }
}