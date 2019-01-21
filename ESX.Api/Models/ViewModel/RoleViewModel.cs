using System.ComponentModel.DataAnnotations;

namespace ESX.Api.Models.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        public int RoleId { get; set; }
    }
}