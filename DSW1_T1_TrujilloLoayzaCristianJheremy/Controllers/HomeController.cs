using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DSW1_T1_TrujilloLoayzaCristianJheremy.Models;

namespace DSW1_T1_TrujilloLoayzaCristianJheremy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Libro");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
