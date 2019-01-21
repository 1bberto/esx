using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Shared.Interfaces;

namespace ESX.Infra.Data.Persistence.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork uow) : base(uow)
        { }
    }
}