using System.ComponentModel.DataAnnotations;

namespace UniversityLibrary.Models
{
    public class BookLoan
    {
        public int ID { get; set; }

        [Required]
        public ICollection<Book>? Books { get; set; }

        [Required]
        public Student? Student { get; set; }

        public int StudentID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }
    }
}
