using CityLibrarySYS_DesignPatterns.Models;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetAll();
        Task Add(Book book);
    }
}
