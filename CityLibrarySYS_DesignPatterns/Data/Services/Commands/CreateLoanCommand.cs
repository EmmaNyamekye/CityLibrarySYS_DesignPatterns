using CityLibrarySYS_DesignPatterns.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityLibrarySYS_DesignPatterns.Data.Services.Commands
{
    // The Concrete Command: binds an action (CreateLoan) to a Receiver (ILoanService).
    public class CreateLoanCommand : ILoanCommand
    {
        // Receiver: The service that performs the business logic
        private readonly ILoanService _loanService;

        // Parameters: The data needed for the action
        private readonly int _memberId;
        private readonly List<Book> _loanCart;

        // Constructor: Injects the Receiver and receives the parameters
        public CreateLoanCommand(ILoanService loanService, int memberId, List<Book> loanCart)
        {
            _loanService = loanService;
            _memberId = memberId;
            _loanCart = loanCart;
        }

        // Execute: Calls the appropriate method on the Receiver
        public async Task Execute()
        {
            // The command logic is simple: delegate the action to the service
            await _loanService.CreateLoan(_memberId, _loanCart);
        }
    }
}