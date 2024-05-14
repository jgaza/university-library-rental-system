using System.ComponentModel.DataAnnotations;

namespace UniversityLibrary.Models
{
    public class Author : Person
    {
        public int ID { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
