using CityLibrarySYS_DesignPatterns.Data;
using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityLibrarySYS_DesignPatterns.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDatabaseContext _context;
        public BookController(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var book = await _context.Books.ToListAsync();
            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return RedirectToAction("index");
            }
            return View(book);
        }

    }
}
