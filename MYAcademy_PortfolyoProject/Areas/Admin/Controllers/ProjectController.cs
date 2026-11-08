using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;
using Microsoft.EntityFrameworkCore;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolyoContext _context;

        public ProjectController(PortfolyoContext context)
        {
            _context = context;
        }

        public IActionResult ProjectList()
        {
            var values = _context.Projects.Include(x => x.Category).ToList();
            return View(values);
        }
    }
}