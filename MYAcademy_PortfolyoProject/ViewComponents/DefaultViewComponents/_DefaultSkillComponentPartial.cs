using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolyoProject.Context;

namespace MYAcademy_PortfolyoProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultSkillComponentPartial : ViewComponent
    {
        private readonly PortfolyoContext _context;

        public _DefaultSkillComponentPartial(PortfolyoContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Skills
                .Include(x => x.SkillItems)
                .OrderBy(x => x.DisplayOrder)
                .ToList();
            return View(values);
        }
    }
}
