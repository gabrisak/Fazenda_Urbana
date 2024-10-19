using Microsoft.AspNetCore.Mvc;

namespace PIM.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserLogin()
        {
            return View();
        }
        public IActionResult ForgotUser()
        {
            return View();
        }
        public IActionResult NewUser()
        {
            return View();
        }
    }
}
