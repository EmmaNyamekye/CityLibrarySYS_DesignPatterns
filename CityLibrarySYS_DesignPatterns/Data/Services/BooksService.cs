using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.EntityFrameworkCore;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public class BooksService : IBooksService
    {
        private readonly LibraryDatabaseContext _context;
        public BooksService(LibraryDatabaseContext context)
        {
            _context = context;
        }
        public async Task Add(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var book = await _context.Books.ToListAsync();
            return book;
        }
    }
}
