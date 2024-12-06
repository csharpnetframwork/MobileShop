using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

public class Login : Controller
{
    private readonly ApplicationDbContext _context;

    public Login(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        // Pass session data to the view
        ViewData["Username"] = HttpContext.Session.GetString("Username");
        return View();
    }

    [HttpPost]
    public IActionResult Index(RegisterModel registerModel)
    {
        if (registerModel.Password !=null  && registerModel.Username !=null)
        {
            var login = _context.RegisterModel
                .FirstOrDefault(x => x.Username == registerModel.Username && x.Password == registerModel.Password && x.IsApproved.Equals(true));

            if (login != null)
            {
                HttpContext.Session.SetString("Username", login.Username);
                return RedirectToAction("Index","Stock");
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
        }
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}
