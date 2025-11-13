using CityLibrarySYS_DesignPatterns.Data.Services;
using CityLibrarySYS_DesignPatterns.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityLibrarySYS_DesignPatterns.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _service;

        public MemberController(IMemberService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllMembers();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Forename,Surname,DoB,Street,Town,CountyCode,Eircode,Phone,Email")] Member member)
        {
            if (!ModelState.IsValid)
            {
                return View(member);
            }
            await _service.AddMember(member);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var memberDetails = await _service.GetMemberById(id);
            if (memberDetails == null) return View("NotFound");
            return View(memberDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var memberDetails = await _service.GetMemberById(id);
            if (memberDetails == null) return View("NotFound");
            return View(memberDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("MemberID,Forename,Surname,DoB,Street,Town,CountyCode,Eircode,Phone,Email,Status")] Member member)
        {
            if (id != member.MemberID)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                return View(member);
            }

            await _service.UpdateMember(id, member);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var memberDetails = await _service.GetMemberById(id);
            if (memberDetails == null) return View("NotFound");
            return View(memberDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberDetails = await _service.GetMemberById(id);
            if (memberDetails == null) return View("NotFound");

            await _service.DeleteMember(id);
            return RedirectToAction(nameof(Index));
        }
    }
}