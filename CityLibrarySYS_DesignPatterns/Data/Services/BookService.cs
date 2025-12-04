using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.EntityFrameworkCore;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    // The core component in the Decorator/Caching pattern
    public class BookService : IBookService
    {
        private readonly LibraryDatabaseContext _context;

        public BookService(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.OrderBy(b => b.Title).ToListAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.BookID == id);
        }

        public async Task AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        // Implementation for UpdateBook(int id, Book book)
        public async Task UpdateBook(int id, Book book)
        {
            var existingBook = await GetBookById(id);
            if (existingBook == null) return;

            // Update properties from the incoming book object
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            existingBook.Status = book.Status; // Status update is also possible via UpdateBook

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();
        }

        // --- REMOVED: Task DeleteBook(int id) implementation ---

        // Implementation for FindMatchingAvailableBooks(string searchTerm)
        public async Task<IEnumerable<Book>> FindMatchingAvailableBooks(string searchTerm)
        {
            // Searches for available ('A') books matching title or author
            var books = await _context.Books
                .Where(b => b.Status == 'A' && (b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm)))
                .ToListAsync();
            return books;
        }

        // Implementation for ChangeBookStatus(int bookId, char newStatus)
        public async Task ChangeBookStatus(int bookId, char newStatus)
        {
            var book = await GetBookById(bookId);
            if (book != null)
            {
                book.Status = newStatus;
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}