using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("ExperienceList", "Experience", new { area = "Admin" });
        }
    }
}
