
namespace CityLibrarySYS_DesignPatterns.Models
{
    public class Book
    {
        public int BookID { get; set; } = 0;
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime Pubblication { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
        public char Status { get; set; } = 'A';


    }
}
