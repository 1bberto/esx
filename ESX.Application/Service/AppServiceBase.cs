using System.Collections.Generic;
using System.Threading.Tasks;
using ESX.Application.Interfaces;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;

namespace ESX.Application.Service
{
    public class AppServiceBase<T, S, R> : IAppServiceBase<T, S, R>
        where T : class
        where S : IServiceBase<T, R>
        where R : IRepositoryBase<T>
    {
        protected readonly IServiceBase<T, R> _serviceBase;

        public AppServiceBase(IServiceBase<T, R> serviceBase)
        {
            _serviceBase = serviceBase;
        }
        public S GetService()
        {
            return (S)_serviceBase;
        }

        public virtual async Task DeleteAsync(object objId)
        {
            await GetService().DeleteAsync(objId);
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await GetService().GetAllAsync();
        }

        public virtual async Task<T> GetByIdAsync(object objId)
        {
            return await GetService().GetByIdAsync(objId);
        }
    }
}