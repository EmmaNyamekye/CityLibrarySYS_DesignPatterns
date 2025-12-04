using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityLibrarySYS_DesignPatterns.Models
{
    // Tracks the history of a member's status (Active/Inactive)
    // This allows the MemberService to check the most recent status and its expiry date.
    public class MemberStatus
    {
        [Key]
        public int MemberStatusId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        [StringLength(1)]
        public char Status { get; set; } = 'A'; // 'A' = Active, 'I' = Inactive

        [Required]
        public DateTime DateAssigned { get; set; }

        // The date when the 'Inactive' status automatically expires.
        public DateTime InactiveUntil { get; set; }

        [StringLength(255)]
        public string Reason { get; set; } = string.Empty;

        // Navigation Property: Links back to the Member (Many-to-One)
        [ForeignKey("MemberId")]
        public Member Member { get; set; } = null!;
    }
}