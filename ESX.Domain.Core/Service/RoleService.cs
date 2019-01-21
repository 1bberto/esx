using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;

namespace ESX.Domain.Core.Service
{
    public class RoleService : ServiceBase<Role, IRoleRepository>, IRoleService
    {
        public RoleService(IRoleRepository repository) : base(repository)
        {
        }
    }
}