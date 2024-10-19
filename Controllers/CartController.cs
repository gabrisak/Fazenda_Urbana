using Microsoft.AspNetCore.Mvc;

namespace PIM.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
    }
}
