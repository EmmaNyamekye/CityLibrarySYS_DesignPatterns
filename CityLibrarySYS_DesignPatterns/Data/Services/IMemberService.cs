using System;
using System.Threading.Tasks;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public interface IMemberService
    {
        // 1. Core Status Check Logic for Pre-Loan Validation:
        // Checks the current status based on the history table. 
        // If the inactive period has expired, it clears the status to 'A' and returns true.
        // Returns (canBorrow, message).
        Task<(bool CanBorrow, string Message)> CheckAndClearInactiveStatus(int memberId);

        // 2. Core Status Update Logic for Observer Pattern:
        // Creates a new MemberStatus record to log the status change.
        Task UpdateMemberStatus(int memberId, char newStatus, DateTime inactiveUntil, string reason);
    }
}