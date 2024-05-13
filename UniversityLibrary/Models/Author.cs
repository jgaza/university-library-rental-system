using System.ComponentModel.DataAnnotations;

namespace UniversityLibrary.Models
{
    public class Author : Person
    {
        public ICollection<Book>? Books { get; set; }
    }
}
