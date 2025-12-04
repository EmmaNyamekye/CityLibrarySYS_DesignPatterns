using CityLibrarySYS_DesignPatterns.Data.Services; // Needed for IMemberService
using System.Threading.Tasks;
using System;

namespace CityLibrarySYS_DesignPatterns.Data.Services.Observers
{
    // Concrete Observer that listens for loan events and acts on member status
    public class MemberStatusObserver : INotifier // Now implements INotifier
    {
        private readonly IMemberService _memberService;

        public MemberStatusObserver(IMemberService memberService)
        {
            _memberService = memberService;
            // IMPORTANT: The subscription logic is now handled in LoanService.cs 
            // via the Func<T, Task> event setup and Dependency Injection.
        }

        // Implementation of the observer's handler method
        public async Task HandleLoanEvent(LoanEvent loanEvent)
        {
            // Only respond to late returns and if the late duration is greater than 0
            if (loanEvent.EventType == "BookReturnedLate" && loanEvent.LateDurationDays > 0)
            {
                // Core business logic: Member gets set to 'Inactive' for the exact duration they were late
                var inactiveUntilDate = DateTime.Now.AddDays(loanEvent.LateDurationDays);
                var reason = $"Inactivated due to late return of Loan ID: {loanEvent.LoanId}. Fine period: {loanEvent.LateDurationDays} days.";

                await _memberService.UpdateMemberStatus(
                    loanEvent.MemberId,
                    'I',
                    inactiveUntilDate,
                    reason
                );
            }
        }
    }
}