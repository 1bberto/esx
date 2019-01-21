using ESX.Domain.Core.Generic;
using System;
using ColumnAttribute = ESX.Domain.Shared.Attributes.ColumnAttribute;

namespace ESX.Domain.Core.Entity
{
    public class BaseEntity : IEntity
    {
        [Column("DataInclusaoRegistro")]
        public DateTime DataInclusaoRegistro { get; set; }
    }
}