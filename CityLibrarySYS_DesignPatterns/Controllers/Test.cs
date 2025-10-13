using Microsoft.AspNetCore.Mvc;
using CityLibrarySYS_DesignPatterns.Data;
using System.Linq;

public class MembersController : Controller
{
    private readonly LibraryDatabaseContext _context;

    public MembersController(LibraryDatabaseContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var members = _context.Members.ToList();
        return View(members);
    }
}
