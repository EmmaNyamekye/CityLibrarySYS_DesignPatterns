using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityLibrarySYS_DesignPatterns.Models
{
    public class LoanItem
    {
        [Key]
        public int LoanItemId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public DateTime LoanDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Required]
        [StringLength(1)]
        public char Status { get; set; } = 'O';

        [ForeignKey("BookId")]
        public Book? Book { get; set; }

        [ForeignKey("MemberId")]
        public Member? Member { get; set; }
    }
}
