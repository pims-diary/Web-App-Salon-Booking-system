using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Salon.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Dashboard()
        {
            // ✅ If staff is NOT logged in (no session), return 401
            if (HttpContext.Session.GetString("LoggedInStaff") == null)
            {
                // This will be caught by UseStatusCodePagesWithReExecute
                // and send the user to Views/Error/401.cshtml
                return StatusCode(401);
            }

            return View();
        }
    }
}
