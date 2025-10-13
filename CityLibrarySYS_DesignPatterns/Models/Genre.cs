using System.ComponentModel.DataAnnotations;

namespace CityLibrarySYS_DesignPatterns.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; } = 0;
        public string Description { get; set; } = string.Empty;

    }
}
