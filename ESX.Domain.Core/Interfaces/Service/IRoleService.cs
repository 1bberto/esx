using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;

namespace ESX.Domain.Core.Interfaces.Service
{
    public interface IRoleService : IServiceBase<Role, IRoleRepository>
    {
    }
}