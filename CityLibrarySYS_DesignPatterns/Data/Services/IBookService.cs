using System.Collections.Generic;
using System.Threading.Tasks;
using CityLibrarySYS_DesignPatterns.Models;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book?> GetBookById(int id);
        Task AddBook(Book book);

        // Matches the signature reported by the compiler previously
        Task UpdateBook(int id, Book book);

        // --- REMOVED: Task DeleteBook(int id); ---

        // Core functionality for searching available books
        Task<IEnumerable<Book>> FindMatchingAvailableBooks(string searchTerm);

        // Functionality for inventory/loan status management
        Task ChangeBookStatus(int bookId, char newStatus);
    }
}