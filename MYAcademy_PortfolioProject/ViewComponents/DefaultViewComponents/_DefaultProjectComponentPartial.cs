using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Sitede yalnızca AKTİF projeler, en yeniden eskiye listelenir.
            var projects = _context.Projects
                .Include(x => x.Category)
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.ProjectDate)
                .ToList();

            // Filtre butonları: sadece aktif projesi olan kategoriler
            ViewBag.Categories = projects
                .Where(x => x.Category != null)
                .Select(x => x.Category!)
                .DistinctBy(x => x.CategoryId)
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View(projects);
        }
    }
}
