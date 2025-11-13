using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.EntityFrameworkCore;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDatabaseContext _context;

        public BookService(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.BookID == id);
        }

        public async Task UpdateBook(int id, Book newBook)
        {
            var existingBook = await GetBookById(id);
            if (existingBook == null) return;

            existingBook.ISBN = newBook.ISBN;
            existingBook.Title = newBook.Title;
            existingBook.Author = newBook.Author;
            existingBook.Genre = newBook.Genre;
            existingBook.Publication = newBook.Publication;
            existingBook.Description = newBook.Description;
            existingBook.Status = newBook.Status;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await GetBookById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}