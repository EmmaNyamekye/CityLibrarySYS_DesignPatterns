using System.Collections.Generic;
using System.Threading.Tasks;
using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace CityLibrarySYS_DesignPatterns.Data.Services
{
    // The Decorator that adds caching capabilities to the IBookService
    public class CachingBookService : IBookService
    {
        private readonly BookService _coreService;
        private readonly IMemoryCache _cache;
        private const string AllBooksCacheKey = "AllBooksList";

        public CachingBookService(BookService coreService, IMemoryCache cache)
        {
            _coreService = coreService;
            _cache = cache;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            // Caching logic
            if (!_cache.TryGetValue(AllBooksCacheKey, out IEnumerable<Book>? books))
            {
                books = await _coreService.GetAllBooks();
                _cache.Set(AllBooksCacheKey, books, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = System.TimeSpan.FromMinutes(5)
                });
            }
            return books ?? Enumerable.Empty<Book>();
        }

        public Task<Book?> GetBookById(int id)
        {
            return _coreService.GetBookById(id);
        }

        public async Task AddBook(Book book)
        {
            await _coreService.AddBook(book);
            _cache.Remove(AllBooksCacheKey); // Invalidate cache on change
        }

        public async Task UpdateBook(int id, Book book)
        {
            await _coreService.UpdateBook(id, book);
            _cache.Remove(AllBooksCacheKey); // Invalidate cache on change
        }

        // --- REMOVED: Task DeleteBook(int id) implementation ---

        public Task<IEnumerable<Book>> FindMatchingAvailableBooks(string searchTerm)
        {
            // Search is a read-only operation and results are dynamic, proxy directly
            return _coreService.FindMatchingAvailableBooks(searchTerm);
        }

        public async Task ChangeBookStatus(int bookId, char newStatus)
        {
            await _coreService.ChangeBookStatus(bookId, newStatus);
            _cache.Remove(AllBooksCacheKey); // Invalidate cache on change
        }
    }
}