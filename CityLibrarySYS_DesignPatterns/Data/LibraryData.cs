using Microsoft.EntityFrameworkCore;

namespace CityLibrarySYS_DesignPatterns.Data
{
    public class LibraryData : DbContext
    {
        public LibraryData(DbContextOptions<LibraryData> options): base(options) { }

        DbSet<Models.Library> Libraries { get; set; }

    }
}
