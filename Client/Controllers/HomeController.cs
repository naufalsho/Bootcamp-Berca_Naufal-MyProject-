using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        //[Authentication]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            //if (HttpContext.Session.GetString("email") == null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Departments");
            //}
            return View();
        }

        //// auth untuk cara session
        //[HttpPost]
        //public IActionResult Login(string Email)
        //{
        //    if (HttpContext.Session.GetString("email") == null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            HttpContext.Session.SetString("email", Email.ToString());
        //            return RedirectToAction("Index", "Departments");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    return View();
        //}
        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Remove("token");
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Login");
        //}
    }

}