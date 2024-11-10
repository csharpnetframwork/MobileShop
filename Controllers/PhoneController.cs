using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShop.Models;

namespace MobileShop.Controllers
{
   // [Authorize(Roles = "Admin, User, LimitedUser")]
    public class PhoneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Phones.ToListAsync());
        }

        [Authorize(Roles = "Admin, User")]
        public IActionResult AddPhone()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> AddPhone(Phone phone)
        {
            phone.AddedDate = DateTime.Now;
            _context.Phones.Add(phone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
