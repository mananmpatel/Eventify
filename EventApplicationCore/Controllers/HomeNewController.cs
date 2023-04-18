using Microsoft.AspNetCore.Mvc;

namespace EventApplicationCore.Controllers
{
    public class HomeNewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
