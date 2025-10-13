using System.ComponentModel.DataAnnotations;

namespace CityLibrarySYS_DesignPatterns.Models
{
    public class Member
    {
        [Key]
        public int MemberID { get; set; } = 0;
        public string Forename { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime DoB { get; set; } = DateTime.Now;
        public string Street { get; set; } = string.Empty;
        public string Town { get; set; } = string.Empty;
        public string County { get; set; } = string.Empty;
        public string Eircode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
