using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShop.Models;

namespace MobileShop.Controllers
{
    [Authorize(Roles = "Admin, User, LimitedUser")]
    public class SaleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SaleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sales.ToListAsync());
        }

        [Authorize(Roles = "User, LimitedUser")]
        public IActionResult RecordSale()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User, LimitedUser")]
        public async Task<IActionResult> RecordSale(Sale sale)
        {
            sale.SaleDate = DateTime.Now;
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
