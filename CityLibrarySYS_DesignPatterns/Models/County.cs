using System.ComponentModel.DataAnnotations;

namespace CityLibrarySYS_DesignPatterns.Models
{
    public class County
    {
        [Key]
        public string CountyCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
