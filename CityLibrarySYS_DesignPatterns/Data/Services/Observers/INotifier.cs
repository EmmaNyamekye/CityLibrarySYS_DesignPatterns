using System.Threading.Tasks;

namespace CityLibrarySYS_DesignPatterns.Data.Services.Observers
{
    // The data payload for any loan-related event, passed to the Observers
    public class LoanEvent
    {
        public int MemberId { get; set; }
        public int LoanId { get; set; }
        public string EventType { get; set; } = null!; // e.g., "LoanCreated", "BookReturnedLate"
        public string Message { get; set; } = string.Empty;

        // --- NEW: Required for late-return logic ---
        // This holds the exact duration (in days) the book was late.
        public int LateDurationDays { get; set; } = 0;
    }

    // The common interface for all Concrete Observers
    public interface INotifier
    {
        // The method subscribed to by the Subject (LoanService)
        Task HandleLoanEvent(LoanEvent loanEvent);
    }
}