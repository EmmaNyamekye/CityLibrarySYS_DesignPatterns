
namespace CityLibrarySYS_DesignPatterns.Models
{
    public class Book
    {
        public string bookID { get; set; } = "";
        public string ISBN { get; set; } = "";
        public string title { get; set; } = "";
        public string author { get; set; } = "";
        public string genre { get; set; } = "";
        public DateTime pubblication { get; set; } = DateTime.Now;
        public string description { get; set; } = "";
        public int LibraryID { get; set; }
        public char status { get; set; }


    }
}
