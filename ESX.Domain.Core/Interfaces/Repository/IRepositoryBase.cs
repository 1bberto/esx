using ESX.Domain.Core.Generic;

namespace ESX.Domain.Core.Interfaces.Repository
{
    public interface IRepositoryBase<T> :
        IGetUow,
        IGet<T>,
        IDelete<T> where T : class
    { }
}