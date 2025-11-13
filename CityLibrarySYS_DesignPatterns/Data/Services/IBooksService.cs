using CityLibrarySYS_DesignPatterns.Models;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public interface IBookService
    {
        Task AddBook(Book book);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book?> GetBookById(int id);
        Task UpdateBook(int id, Book newBook);
        Task DeleteBook(int id);
    }
}