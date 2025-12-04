namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    // This is the Singleton implementation, holding immutable loan policies.
    public class LoanRules : ILoanRules
    {
        // Fixed application rules
        public int MaxBooksPerMember => 6;
        public int LoanDurationDays => 30; // 30 days loan period
        
        public LoanRules() { }
    }
}