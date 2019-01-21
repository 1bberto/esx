using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = ESX.Domain.Shared.Attributes.ColumnAttribute;

namespace ESX.Domain.Core.Entity
{
    [Table("tblRole")]
    public class Role : BaseEntity
    {
        [Column("RoleId")]
        [Key]
        public int RoleId { get; set; }
        [Column("Nome")]
        public string Nome { get; set; }
    }
}