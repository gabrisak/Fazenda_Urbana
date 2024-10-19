using Microsoft.AspNetCore.Mvc;

namespace PIM.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
