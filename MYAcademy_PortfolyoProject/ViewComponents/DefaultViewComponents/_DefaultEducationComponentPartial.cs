using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;

namespace MYAcademy_PortfolyoProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultEducationComponentPartial : ViewComponent
    {
        private readonly PortfolyoContext _context;

        public _DefaultEducationComponentPartial(PortfolyoContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Sitede yalnızca aktif kayıtlar, belirlenen sıraya göre gösterilir.
            var values = _context.Educations
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ThenByDescending(x => x.StartYear)
                .ToList();
            return View(values);
        }
    }
}
