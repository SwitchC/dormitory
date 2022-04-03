using dormitory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Security.Claims;
namespace dormitory.Controllers
{
    [Authorize( Roles="employee,student")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewBag.id = Int32.Parse(HttpContext.User.Identity.Name); ;
            ViewBag.User=HttpContext.User.Claims.FirstOrDefault(x=>x.Type==ClaimsIdentity.DefaultRoleClaimType).Value;

            return View();
        }
        public IActionResult Dormitories()
        {
            return RedirectToAction("Index", "Dormitories");
        }
        public IActionResult Students1(int id)
        {
            return RedirectToAction("Index", "Students1", new {id=id });
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
    }
}