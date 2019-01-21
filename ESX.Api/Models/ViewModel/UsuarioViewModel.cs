using System.ComponentModel.DataAnnotations;

namespace ESX.Api.Models.ViewModel
{
    public class UsuarioViewModel : IViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}