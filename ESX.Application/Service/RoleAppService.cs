using ESX.Application.Interfaces;
using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;

namespace ESX.Application.Service
{
    public class RoleAppService :
        AppServiceBase<Role, IRoleService, IRoleRepository>,
        IRoleAppService
    {
        public RoleAppService(IRoleService service) : base(service)
        { }
    }
}