using ESX.Domain.Shared.Interfaces;

namespace ESX.Domain.Core.Generic
{
    public interface IGetUow
    {
        IUnitOfWork GetUow();
    }
}