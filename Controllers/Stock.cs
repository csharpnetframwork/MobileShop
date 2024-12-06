using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
   
    public class Stock : Controller
    {
        private readonly ApplicationDbContext _context;

        public Stock(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            ViewData["Username"] = HttpContext.Session.GetString("Username");
            var stockItems = _context.Stocks.Where(x=>x.iteminstock.Equals(true)).ToList(); // Load stock items from the database
            var viewModel = new StockViewModel
            {
                StockItems = stockItems,
                NewStockItem = new StockItem() // Initialize an empty StockItem for the form
            };

            return View(viewModel); // Pass the view model to the view
          
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(StockViewModel model)
        {
            if (model.NewStockItem == null)
            {
                ModelState.AddModelError("", "Invalid item submission.");
                return View("Index", model); // Reload the view with the current model
            }
             Guid Id= Guid.NewGuid();
            // Proceed if model binding is successful
            var newItem = new StockItem
            {   Id=Id,
                Name = model.NewStockItem.Name,
                Model = model.NewStockItem.Model,
                Specification = model.NewStockItem.Specification,
                Rate = model.NewStockItem.Rate,
                PurchaseDate = model.NewStockItem.PurchaseDate,
                iteminstock = model.NewStockItem.iteminstock,
                ModifyBy = model.NewStockItem.ModifyBy,
                ModifyDate = DateTime.Now.ToString()
            };

            // Save to the database
            _context.Stocks.Add(newItem);
            await _context.SaveChangesAsync();

            // Redirect back to the index action
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SellItem(Guid id)
        {
            // Fetch the item from the Stock database using the provided ID
            var stockItem = _context.Stocks.FirstOrDefault(item => item.Id == id);

            if (stockItem == null)
            {
                // If the item is not found, return to the index page with an error message
                TempData["Error"] = "Stock item not found.";
                return RedirectToAction("Index");
            }

            // Create a new Sell record using the existing stock item's ID
            var sellItem = new Sell
            {
                Id = stockItem.Id, // Reuse the ID from the existing stock item
                Name = stockItem.Name,
                Model = stockItem.Model,
                Specification = stockItem.Specification,
                Rate = stockItem.Rate,
                SellDate = DateTime.Now, // Set the current date and time as the SellDate
                ModifyBy = stockItem.ModifyBy,
                ModifyDate = DateTime.Now.ToString(),
                iteminstock = false // Mark the item as sold
            };

            // Save the Sell record to the database
            _context.Sells.Add(sellItem);

            // Remove the stock item from the Stock table
            _context.Stocks.Remove(stockItem);

            // Save all changes
            _context.SaveChanges();

            // Redirect to the Index view with a success message
            TempData["Success"] = "Item successfully sold and removed from stock.";
            return RedirectToAction("Index");
        }

        public IActionResult SellRecords(string dateRange = "30", string search = "")
        {
            // Implement your logic to fetch and filter sell records
            var records = _context.Sells
                .Where(r => search == "" || r.Name.Contains(search) || r.Model.Contains(search))
                .OrderByDescending(r => r.SellDate)
                .ToList();

            return View(records);
        }
    }
}
