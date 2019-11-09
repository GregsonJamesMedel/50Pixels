using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ErrorNotFound() => View();
    }
}