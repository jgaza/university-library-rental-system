using System.ComponentModel.DataAnnotations;

namespace UniversityLibrary.Models
{
    public class Address
    {
        public int ID { get; set; }

        [Required]
        public string? Street { get; set; }

        [Required]
        [Display(Name = "House Number")]
        public int HouseNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int Postcode { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
