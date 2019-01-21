using System;
using System.Data;

namespace ESX.Domain.Shared.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void RollBack();
        IDbConnection GetConnection();
        IDbTransaction GeTransaction();
        void Release();
    }
}