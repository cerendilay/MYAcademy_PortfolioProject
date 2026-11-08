using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;

namespace MYAcademy_PortfolyoProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultExperienceComponentPartial : ViewComponent
    {
        private readonly PortfolyoContext _context;

        public _DefaultExperienceComponentPartial(PortfolyoContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Sitede yalnızca aktif deneyimler, belirlenen sıraya göre gösterilir.
            var values = _context.Experiences
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ThenByDescending(x => x.ExperienceId)
                .ToList();
            return View(values);
        }
    }
}