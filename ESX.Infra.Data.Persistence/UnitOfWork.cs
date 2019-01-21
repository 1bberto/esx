using ESX.Domain.Shared;
using ESX.Domain.Shared.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace ESX.Infra.Data.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseConnection _databaseConnection;
        public IDbConnection Connection { get; protected set; }
        public IDbTransaction Transaction { get; protected set; }

        public UnitOfWork(IOptions<DatabaseConnection> databaseConnection)
        {
            _databaseConnection = databaseConnection.Value;
        }
        public void Begin()
        {
            if (Connection == null)
            {
                CreateConnection();
            }

            Transaction = Connection.BeginTransaction();
        }
        private void CreateConnection()
        {
            Connection = new SqlConnection(_databaseConnection.Connection);
            Connection.Open();
        }

        public void Commit()
        {
            try
            {
                Transaction.Commit();
            }
            catch
            {
                Transaction?.Rollback();
                throw;
            }
            finally
            {
                Transaction?.Dispose();
                Transaction = null;
            }
        }

        public void RollBack()
        {
            try
            {
                Transaction.Rollback();
            }
            finally
            {
                Transaction?.Dispose();
                Transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Connection != null)
                {
                    try
                    {
                        Connection.Close();
                        Connection.Dispose();
                    }
                    catch
                    {
                        //Não tratar
                    }
                }
            }
        }

        ~UnitOfWork()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IDbConnection GetConnection()
        {
            AguardarComando();
            if (Connection != null) return Connection;
            CreateConnection();
            return Connection;
        }

        public IDbTransaction GeTransaction()
        {
            return Transaction;
        }

        public void Release()
        {
            AguardarComando();
        }

        private void AguardarComando()
        {
            if (Connection == null) return;
            while (Connection.State == ConnectionState.Executing || Connection.State == ConnectionState.Fetching)
            {
                Thread.Sleep(500);
            }
        }
    }
}