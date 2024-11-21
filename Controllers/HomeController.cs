using Microsoft.AspNetCore.Mvc;

namespace PIM.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
