using System.ComponentModel.DataAnnotations;

namespace UniversityLibrary.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [Display(Name = "ISBN")]
        public string Isbn { get; set; }

        public int AuthorID { get; set; }
        public string Publisher { get; set; }
    }
}
