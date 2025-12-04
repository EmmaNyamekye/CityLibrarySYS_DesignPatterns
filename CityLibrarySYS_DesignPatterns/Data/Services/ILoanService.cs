using CityLibrarySYS_DesignPatterns.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public interface ILoanService
    {
        // Must match implementation: returns a tuple containing success status and message.
        Task<(bool Success, string Message)> CreateLoan(int memberId, List<Book> loanCart);

        Task<IEnumerable<LoanItem>> GetOutstandingLoanItemsByBookId(int bookId);
        Task ReturnBook(LoanItem loanItem);

        // This must be Task, matching the implementation
        Task BookReturned(LoanItem loanItem);

        // Must match implementation: returns 'int', not 'Task<int>'.
        int GetNextLoanId();
    }

    // ILoanRules definition was removed from here to resolve the duplication error.
    // It is assumed to exist in a single file: ILoanRules.cs.
}