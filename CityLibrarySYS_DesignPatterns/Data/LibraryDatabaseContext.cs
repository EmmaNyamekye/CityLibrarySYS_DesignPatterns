using Microsoft.EntityFrameworkCore;

namespace CityLibrarySYS_DesignPatterns.Data
{
    public class LibraryDatabaseContext : DbContext
    {
        public LibraryDatabaseContext(DbContextOptions<LibraryDatabaseContext> options) : base(options)
        {
        }

        /*public DbSet<Book> Products { get; set; }
        <Book> is your table*/
    }

    /*https://medium.com/@ravitejherwatta/a-step-by-step-process-to-set-up-a-database-connection-in-asp-net-core-mvc-a03ac8b7cc04*/
}
