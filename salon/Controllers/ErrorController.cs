using Microsoft.AspNetCore.Mvc;

namespace Salon.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            if (statusCode == 401) return View("401");
            if (statusCode == 403) return View("403");
            if (statusCode == 404) return View("404");

            return View("500");
        }

        [Route("Error/500")]
        public IActionResult ServerError()
        {
            return View("500");
        }
    }
}
