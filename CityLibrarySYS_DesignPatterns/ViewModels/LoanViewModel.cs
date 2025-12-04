using CityLibrarySYS_DesignPatterns.Models;
using System.ComponentModel.DataAnnotations;

namespace CityLibrarySYS_DesignPatterns.ViewModels
{
    public class LoanViewModel
    {
        [Required(ErrorMessage = "Please enter the Member ID.")]
        [Display(Name = "Member ID")]
        public int? MemberIdInput { get; set; }

        [Display(Name = "Book Title")]
        public string? TitleSearch { get; set; }

        public Member? MemberDetails { get; set; }

        public List<Book> AvailableBooks { get; set; } = new List<Book>();

        public List<Book> LoanCart { get; set; } = new List<Book>();

        public int NextLoanId { get; set; }
    }
}