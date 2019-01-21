using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = ESX.Domain.Shared.Attributes.ColumnAttribute;

namespace ESX.Domain.Core.Entity
{
    [Table("tblPatrimonio")]
    public class Patrimonio : BaseEntity
    {
        [Column("PatrimonioId")]
        [Key]
        public int PatrimonioId { get; set; }
        [Column("MarcaId")]
        public int MarcaId { get; set; }
        public virtual Marca Marca { get; set; }
        [Column("Descricao")]
        public string Descricao { get; set; }
        [Column("NumeroTombo")]
        public string NumeroTombo { get; set; }
    }
}