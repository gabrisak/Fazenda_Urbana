using Microsoft.AspNetCore.Mvc;

namespace PIM.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
