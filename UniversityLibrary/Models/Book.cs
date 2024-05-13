using System.ComponentModel.DataAnnotations;

namespace UniversityLibrary.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Required]
        public string? Title { get; set; }

        [Display(Name = "ISBN")]
        public string? Isbn { get; set; }

        [Required]
        [Display(Name = "Author(s)")]
        public ICollection<Author>? Authors { get; set; }

        public string? Publisher { get; set; }
    }
}
