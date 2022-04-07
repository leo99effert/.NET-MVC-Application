using System.ComponentModel.DataAnnotations;

namespace NextHome.Models
{
    public class Estate
    {
        public int Id { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        [Display(Name = "Asking Price")]
        public int Price { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Type of Estate")]
        public string? TypeOfEstate { get; set; }

        [Display(Name = "Type of Ownership")]
        public string? TypeOfOwnership { get; set; }

        [Required]
        [Display(Name = "Rooms")]
        public int NrOfRooms { get; set; }

        [Required]
        [Display(Name = "KVM")]
        public int Size { get; set; }

        [Required]
        [Display(Name = "Year Built")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Viewing Date")]
        [DataType(DataType.Date)]
        public DateTime ViewDate { get; set; }
        public string? RealtorId { get; set; }
    }
}
