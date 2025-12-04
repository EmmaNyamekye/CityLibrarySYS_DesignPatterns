using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.EntityFrameworkCore;

namespace CityLibrarySYS_DesignPatterns.Data
{
    public class LibraryDatabaseContext : DbContext
    {
        public LibraryDatabaseContext(DbContextOptions<LibraryDatabaseContext> options)
            : base(options)
        {
        }

        // --- Existing Models ---
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<LoanItem> LoanItems { get; set; } = null!;

        // --- NEW: Required for Observer Pattern and Status History ---
        public DbSet<MemberStatus> MemberStatuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite keys, indexes, or relationships here if necessary
            // For example, ensuring the LoanItem has the correct key
            modelBuilder.Entity<LoanItem>().HasKey(li => li.LoanItemId);
        }
    }
}