using System.ComponentModel.DataAnnotations;

namespace NextHome.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
