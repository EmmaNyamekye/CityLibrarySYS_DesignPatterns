using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.EntityFrameworkCore;

namespace CityLibrarySYS_DesignPatterns.Data
{
    public class LibraryDatabaseContext : DbContext
    {
        public LibraryDatabaseContext(DbContextOptions<LibraryDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanItem> LoanItems { get; set; }
    }
}
