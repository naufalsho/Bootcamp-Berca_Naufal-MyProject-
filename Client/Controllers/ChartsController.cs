using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ChartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
