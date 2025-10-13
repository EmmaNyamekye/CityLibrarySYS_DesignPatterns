using System.ComponentModel.DataAnnotations;

namespace CityLibrarySYS_DesignPatterns.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; } = 0;
        public int MemberId { get; set; } = 0;
    }
}
