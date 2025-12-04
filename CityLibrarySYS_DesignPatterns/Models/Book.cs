using System;
using System.ComponentModel.DataAnnotations;

namespace CityLibrarySYS_DesignPatterns.Models
{
    public class Book
    {
        public int BookID { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN must be 10-13 characters")]
        public string ISBN { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string Genre { get; set; } = null!;

        [Required]
        public DateTime Publication { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(1)]
        public char Status { get; set; } = 'A'; // A = Active, I = Inactive
    }
}