using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}