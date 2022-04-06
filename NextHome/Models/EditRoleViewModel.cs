using System.ComponentModel.DataAnnotations;

namespace NextHome.Models
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }

        public string? Id { get; set; }

        [Required(ErrorMessage = "RoleName is Required")]
        public string? RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
