using Dapper;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Shared.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ESX.Infra.Data.Persistence.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly IUnitOfWork Uow;
        protected readonly string TableName;
        protected readonly string InsertProcedure;
        protected readonly string SelectProcedure;
        protected readonly string UpdateProcedure;
        protected readonly string DeleteProcedure;

        static RepositoryBase()
        {
            Initiator.RegisterModels();
        }
        protected RepositoryBase(IUnitOfWork uow)
        {
            Uow = uow;
            TableName = ClassToTable(typeof(T).Name);
            InsertProcedure = $"USP_{typeof(T).Name}_INS";
            SelectProcedure = $"USP_{typeof(T).Name}_SEL";
            UpdateProcedure = $"USP_{typeof(T).Name}_UPD";
            DeleteProcedure = $"USP_{typeof(T).Name}_DEL";
        }

        public IUnitOfWork GetUow()
        {
            return Uow;
        }
        protected string ClassToTable(string className)
        {
            if (typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() is TableAttribute tableAttr)
                return tableAttr.Name.Replace("tbl", "");

            return $"tbl{className}";
        }
        public static string GetKeyField()
        {
            return typeof(T).GetProperties()
                .FirstOrDefault(x => x.GetCustomAttribute<ESX.Domain.Shared.Attributes.ColumnAttribute>() != null &&
                                     x.GetCustomAttribute<KeyAttribute>() != null)
                ?.GetCustomAttribute<Domain.Shared.Attributes.ColumnAttribute>().Name;
        }

        public virtual async Task<T> GetByIdAsync(object objId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add($"@{GetKeyField()}", objId);
                var ret = await Uow.GetConnection().QueryFirstOrDefaultAsync<T>(
                    $"EXEC {SelectProcedure} @{GetKeyField()}", param, Uow.GeTransaction()
                );
                return ret;
            }
            finally
            {
                Uow.Release();
            }
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            try
            {
                var ret = await Uow.GetConnection().QueryAsync<T>(
                    $"EXEC {SelectProcedure}", null, Uow.GeTransaction());
                return ret.AsList();
            }
            finally
            {
                Uow.Release();
            }
        }

        public virtual async Task DeleteAsync(object objId)
        {
            var param = new DynamicParameters();
            param.Add($"@{GetKeyField()}", objId);
            await Uow.GetConnection().ExecuteAsync($"EXEC {DeleteProcedure} @{GetKeyField()}", param, Uow.GeTransaction());
        }
    }
}