using System.ComponentModel.DataAnnotations;

namespace UniversityLibrary.Models
{
    public class Student : Person
    {
        [Key]
        public int RegistrationID { get; set; }

        [Required]
        public Address? Address { get; set; }

        public ICollection<BookLoan>? BookLoans { get; set; }
    }
}
