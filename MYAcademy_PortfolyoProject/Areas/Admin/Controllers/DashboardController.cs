using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolyoContext _context;

        public DashboardController(PortfolyoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.v1 = _context.Contacts.Count();
            ViewBag.v2 = _context.Messages.Where(x => x.IsRead == false).Count();
            ViewBag.v3 = _context.Messages.Where(x => x.IsRead == true).Count();

            return View();
        }
    }
}