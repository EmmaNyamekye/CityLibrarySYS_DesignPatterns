using CityLibrarySYS_DesignPatterns.Data.Services;
using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityLibrarySYS_DesignPatterns.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllBooks();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ISBN,Title,Author,Publication,Genre,Description,Status")] Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            await _service.AddBook(book);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await _service.GetBookById(id);
            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetBookById(id);
            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,ISBN,Title,Author,Publication,Genre,Description,Status")] Book book)
        {
            if (id != book.BookID) return View("NotFound");

            if (!ModelState.IsValid)
            {
                return View(book);
            }

            await _service.UpdateBook(id, book);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bookDetails = await _service.GetBookById(id);
            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookDetails = await _service.GetBookById(id);
            if (bookDetails == null) return View("NotFound");

            await _service.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }
    }
}