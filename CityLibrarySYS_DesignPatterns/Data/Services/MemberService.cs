using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public class MemberService : IMemberService
    {
        private readonly LibraryDatabaseContext _context;

        public MemberService(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Member?> GetMemberById(int memberId)
        {
            // Includes the most recent status record for easy access
            return await _context.Members
                // Includes the status history collection
                .Include(m => m.Statuses)
                .FirstOrDefaultAsync(m => m.MemberId == memberId);
        }

        // Implementation of IMemberService.CheckAndClearInactiveStatus
        public async Task<(bool CanBorrow, string Message)> CheckAndClearInactiveStatus(int memberId)
        {
            var member = await GetMemberById(memberId);
            if (member == null) return (false, "Member not found.");

            // Get the most recent status record from the history
            var currentStatus = member.Statuses?.OrderByDescending(s => s.DateAssigned).FirstOrDefault();

            // If the member has no status history, they are considered 'Active'
            if (currentStatus == null || currentStatus.Status == 'A')
            {
                return (true, "Member status is Active. Loan can proceed.");
            }

            // Check if the current status is Inactive ('I')
            if (currentStatus.Status == 'I')
            {
                // Check if the inactive period has passed
                if (currentStatus.InactiveUntil <= DateTime.Now)
                {
                    // Status has expired, change member status back to Active ('A')
                    // Note: We use DateTime.MinValue as the inactiveUntil date for an 'Active' status
                    await UpdateMemberStatus(memberId, 'A', DateTime.MinValue, "Inactive period expired and status reset.");
                    return (true, "Inactive status cleared. Loan can proceed.");
                }
                else
                {
                    // Status is still active, deny the loan
                    var remainingDays = Math.Ceiling((currentStatus.InactiveUntil - DateTime.Now).TotalDays);
                    return (false, $"Loan denied: Member is inactive until {currentStatus.InactiveUntil.ToShortDateString()}. Remaining: {remainingDays} days.");
                }
            }

            // Default safe return, should be unreachable if logic is clean
            return (true, "Member status is Active. Loan can proceed.");
        }

        // Implementation of IMemberService.UpdateMemberStatus (Used by Observer)
        public async Task UpdateMemberStatus(int memberId, char newStatus, DateTime inactiveUntil, string reason)
        {
            // Create a new status record in the history table
            var newStatusRecord = new MemberStatus
            {
                MemberId = memberId,
                Status = newStatus,
                DateAssigned = DateTime.Now,
                InactiveUntil = inactiveUntil,
                Reason = reason
            };

            await _context.MemberStatuses.AddAsync(newStatusRecord);
            await _context.SaveChangesAsync();
        }
    }
}