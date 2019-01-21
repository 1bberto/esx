using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = ESX.Domain.Shared.Attributes.ColumnAttribute;

namespace ESX.Domain.Core.Entity
{
    [Table("tblUsuario")]
    public class Usuario : BaseEntity
    {
        [Column("UsuarioId")]
        [Key]
        public string UsuarioId { get; set; }
        [Column("Login")]
        public string Login { get; set; }
        [Column("Senha")]
        public string Senha { get; set; }
        [Column("Nome")]
        public string Nome { get; set; }
        public virtual IList<Role> Roles { get; set; }
    }
}