using Dapper;
using ESX.Domain.Core.Entity;
using ESX.Utils;
using System;
using System.Linq;

namespace ESX.Infra.Data.Persistence
{
    public class Initiator
    {
        private static bool _register = false;
        public static void RegisterModels()
        {
            if (_register) return;

            var tipoPadrao = typeof(Marca);
            var entitidades = tipoPadrao
                .Assembly
                .GetTypes()?
                .Where(type => type.Namespace != null &&
                               type.Namespace.StartsWith(tipoPadrao.Namespace) &&
                               !type.IsGenericType && type.IsClass).ToList();
            entitidades?.ForEach(type =>
            {
                dynamic typeMap =
                    Activator.CreateInstance(typeof(ColumnAttributeTypeMapper<>).MakeGenericType(type));
                SqlMapper.SetTypeMap(type, typeMap);
            });
            _register = true;

            typeof(Initiator).Assembly.GetTypes()?.Where(x =>
                x.IsAssignableToGenericType(typeof(SqlMapper.TypeHandler<>)) && !x.IsAbstract
            )?.ToList().ForEach(x =>
            {
                var handler = Activator.CreateInstance(x);
                SqlMapper.AddTypeHandler(x.BaseType.GenericTypeArguments[0], (SqlMapper.ITypeHandler)handler);// Convert.ChangeType(handler, typeof(SqlMapper.TypeHandler<>)));
            });
        }
    }
}