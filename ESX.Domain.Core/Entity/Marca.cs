using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = ESX.Domain.Shared.Attributes.ColumnAttribute;

namespace ESX.Domain.Core.Entity
{
    [Table("tblMarca")]
    public class Marca : BaseEntity
    {
        [Column("MarcaId")]
        [Key]
        public int MarcaId { get; set; }
        [Column("Nome")]
        public string Nome { get; set; }
    }
}