using System.ComponentModel.DataAnnotations;

namespace CityLibrarySYS_DesignPatterns.Models
{
    public class Library
    {
        public int LibarrayID { get; set; }
        [Required]
        /*[char.IsDigit(char c)]*/
        public string libraryName { get; set; } = "";
        [Required]
        public string street { get; set; } = "";
        [Required]
        public string town { get; set; } = "";
        [Required]
        public string county { get; set; } = "";
        [Required]
        [StringLength(7, ErrorMessage= "Ericode must be 7 characters long!")]
        public string eircode { get; set; } = "";
        [Required]
        public string phone { get; set; } = "";
        [Required]
        public string email { get; set; } = "";
        [Required]
        public string supervisord { get; set; } = "";
    }
}
