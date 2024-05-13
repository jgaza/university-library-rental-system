using System.ComponentModel.DataAnnotations;

namespace UniversityLibrary.Models
{
    public class Publisher
    {
        public int ID { get; set; }

        [Required]
        public string? Name { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
