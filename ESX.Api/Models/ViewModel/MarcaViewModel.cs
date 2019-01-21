using System.ComponentModel.DataAnnotations;

namespace ESX.Api.Models.ViewModel
{
    public class MarcaViewModel : IViewModel
    {
        [Required]
        public string Nome { get; set; }
    }
}