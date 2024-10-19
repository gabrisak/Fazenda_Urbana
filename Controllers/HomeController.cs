using Microsoft.AspNetCore.Mvc;
using PIM.Models;
using System.Diagnostics;

namespace PIM.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

     
    }
}
