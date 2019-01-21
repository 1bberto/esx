using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;

namespace ESX.Application.Interfaces
{
    public interface IRoleAppService : IAppServiceBase<Role, IRoleService, IRoleRepository>
    {
    }
}