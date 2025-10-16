using CityLibrarySYS_DesignPatterns.Data;
using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityLibrarySYS_DesignPatterns.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDatabaseContext _context;
        public BookController(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var book = _context.Books.ToList();
            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] 
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();

                return RedirectToAction("index");
            }
            return View(book);
        }

    }
}
