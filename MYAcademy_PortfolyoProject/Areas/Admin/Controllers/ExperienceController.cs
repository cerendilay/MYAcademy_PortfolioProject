using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
       
        private readonly PortfolyoContext _context;

        public ExperienceController(PortfolyoContext context)
        {
            _context = context;
        }

        public IActionResult ExperienceList()
        {
            var values = _context.Experiences.ToList();
            return View(values);
        }
    }
}
