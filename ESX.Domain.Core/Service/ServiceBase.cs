using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESX.Domain.Core.Service
{
    public class ServiceBase<T, R> : IServiceBase<T, R>
        where T : class
        where R : IRepositoryBase<T>
    {
        protected readonly IRepositoryBase<T> _repository;

        public ServiceBase(IRepositoryBase<T> repository)
        {
            _repository = repository;
        }
        public R GetRepository()
        {
            return (R)_repository;
        }

        public virtual async Task DeleteAsync(object objId)
        {
            await _repository.DeleteAsync(objId);
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<T> GetByIdAsync(object objId)
        {
            return await _repository.GetByIdAsync(objId);
        }
    }
}