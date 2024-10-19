using Microsoft.AspNetCore.Mvc;

namespace PIM.Controllers
{
    public class TestimonialController : Controller
    {
        public IActionResult Testimonial()
        {
            return View();
        }
    }
}
