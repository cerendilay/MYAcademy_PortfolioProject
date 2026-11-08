using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Models;
using System.Diagnostics;

namespace MYAcademy_PortfolyoProject.Controllers
{
    // Portfolyo sitesi DefaultController üzerinden çalışır.
    // Bu controller yalnızca hata sayfası (UseExceptionHandler) için tutulur.
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Default");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
