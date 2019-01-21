using System.ComponentModel.DataAnnotations;

namespace ESX.Api.Models.ViewModel
{
    public class UsuarioLoginViewModel : IViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}