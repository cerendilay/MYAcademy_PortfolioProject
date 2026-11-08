using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_PortfolyoProject.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
