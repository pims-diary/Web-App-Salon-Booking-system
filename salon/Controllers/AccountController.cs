using Microsoft.AspNetCore.Mvc;
using Salon.Data;
using Salon.Models;

namespace Salon.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check in the database if staffId + password match
            var staff = _context.Staffs
                .FirstOrDefault(s => s.StaffId == model.StaffId && s.Password == model.Password);

            if (staff == null)
            {
                // Invalid login
                ModelState.AddModelError(string.Empty, "Invalid Staff ID or Password.");
                return View(model);
            }

            // ✅ Store staff login in Session
            HttpContext.Session.SetString("LoggedInStaff", staff.StaffId);

            // ✅ Login successful – for now, just redirect to Staff dashborad
            // (No session/cookies yet – basic assignment-style check only)
            return RedirectToAction("Dashboard", "Staff");

        }

        public IActionResult Logout()
        {
            // ✅ Clear all session data (log out)
            HttpContext.Session.Clear();

            // ✅ Redirect to landing page (Home/Index)
            return RedirectToAction("Index", "Home");
        }

    }
}
