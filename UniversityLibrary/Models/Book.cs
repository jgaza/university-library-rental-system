namespace UniversityLibrary.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int AuthorID { get; set; }
        public string Publisher { get; set; }
    }
}
