using System.ComponentModel.DataAnnotations;

namespace CityLibrarySYS_DesignPatterns.Models
{
    public class LoanItem
    {
        [Key]
        public int BookID { get; set; } = 0;
        public string LoanID { get; set; } = string.Empty;
        public DateTime LoanStart { get; set; } = DateTime.Now;
        public DateTime LoanEnd { get; set;} = DateTime.Now; /*1 month later*/
        public DateTime RuternDate { get; set;} = DateTime.Now;
        public char Status { get; set; } = 'O';
    }
}
