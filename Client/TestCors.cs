using Microsoft.AspNetCore.Mvc;

namespace Client
{
    public class TestCors : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
