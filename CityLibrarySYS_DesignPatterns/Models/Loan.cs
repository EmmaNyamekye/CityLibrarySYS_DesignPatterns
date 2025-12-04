using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CityLibrarySYS_DesignPatterns.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; } = 0;
        public int MemberId { get; set; } = 0;

        // --- REQUIRED FOR LOANSERVICE LOGIC ---
        public DateTime LoanDate { get; set; } // Required by CreateLoan (Line 53)
        public bool IsReturned { get; set; } = false; // Required by ReturnBook (Loan header status update)

        // Required by CreateLoan and ReturnBook (.Include and .Items.All())
        public List<LoanItem> Items { get; set; } = new List<LoanItem>();
    }
}