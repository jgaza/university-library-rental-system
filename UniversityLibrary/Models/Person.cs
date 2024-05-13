using System.ComponentModel.DataAnnotations;

namespace UniversityLibrary.Models
{
    public abstract class Person
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
    }
}
