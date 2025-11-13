using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CityLibrarySYS_DesignPatterns.Models
{
    public class Member
    {public int MemberID { get; set; }

        [Required(ErrorMessage = "Please enter the member's first name.")]
        [StringLength(20, ErrorMessage = "Forename cannot be longer than 20 characters.")]
        public string Forename { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the member's surname.")]
        [StringLength(20, ErrorMessage = "Surname cannot be longer than 20 characters.")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the date of birth.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [FutureDate(ErrorMessage = "Date of Birth must be a date in the past.")]
        public DateTime DoB { get; set; }

        [Required(ErrorMessage = "Please enter the street address.")]
        [StringLength(25, ErrorMessage = "Street address cannot be longer than 25 characters.")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the town/city.")]
        [StringLength(15, ErrorMessage = "Town/City name cannot be longer than 15 characters.")]
        public string Town { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a valid County Code (e.g., Clare or Dublin).")]
        [StringLength(15, ErrorMessage = "County Code cannot be longer than 15 characters.")]
        [Display(Name = "County Code")]
        public string CountyCode { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the Eircode.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Eircode must be exactly 7 characters long.")]
        [RegularExpression(@"[ACDEFHKPRSTWXY]\d{2}[ACDEFHKPRSTWXY]{4}", ErrorMessage = "Eircode format is invalid. Must be in the format LNN LLLL.")] // Example Eircode format validation
        public string Eircode { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the phone number.")]
        [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 digits/characters.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?\d{9,15}$", ErrorMessage = "Phone must be a valid number, starting optionally with a '+' and containing 9 to 15 digits.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(40, ErrorMessage = "Email cannot be longer than 40 characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email format. Please check the address and try again.")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(1)]
        public string Status { get; set; } = "A"; // A = Active, I = Inactive
    }
}