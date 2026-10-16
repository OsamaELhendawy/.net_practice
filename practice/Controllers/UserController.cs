using Microsoft.AspNetCore.Mvc;
using practice.Models;
using practice.Controllers;
using practice.Context;


namespace practice.Controllers
{
    public class UserController : Controller
    {
        private readonly MyContext db;

        public UserController(MyContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.FirstOrDefault(u => u.email == user.email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("email", "email already used");
                    return View(user);
                }

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string identifier, string password)
        {
            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Identifier and password are required.");
                return View();
            }

            bool isEmail = System.Text.RegularExpressions.Regex.IsMatch(identifier, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            var user = isEmail
                ? db.Users.FirstOrDefault(u => u.email == identifier)
                : db.Users.FirstOrDefault(u => u.UserName == identifier);

            if (user == null || user.Password != password)
            {
                ModelState.AddModelError(string.Empty, "Invalid username/email or password.");
                return View();
            }
            var displayName = string.IsNullOrWhiteSpace(user.UserName) ? user.email : user.UserName;
            TempData["username"] = displayName;

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
