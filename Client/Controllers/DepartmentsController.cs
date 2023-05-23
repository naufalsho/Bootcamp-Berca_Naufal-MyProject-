using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class DepartmentsController : Controller
    {
        //[Authorize]
        //[Authentication]
        public IActionResult Index()
        {
            return View();
        }
    }
}
