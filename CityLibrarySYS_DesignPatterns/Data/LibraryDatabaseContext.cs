using Microsoft.EntityFrameworkCore;
using CityLibrarySYS_DesignPatterns.Models;

namespace CityLibrarySYS_DesignPatterns.Data
{
    public class LibraryDatabaseContext : DbContext
    {
        public LibraryDatabaseContext(DbContextOptions<LibraryDatabaseContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<County> Counties { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;
        public DbSet<LoanItem> LoanItems { get; set; } = null!;
    }
}
