namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public interface ILoanRules
    {
        // Maximum number of books a member can borrow at one time
        int MaxBooksPerMember { get; }

        // Duration of the standard loan in days
        int LoanDurationDays { get; }
    }
}