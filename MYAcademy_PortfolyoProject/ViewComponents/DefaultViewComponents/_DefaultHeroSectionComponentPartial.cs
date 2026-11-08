using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;

namespace MYAcademy_PortfolyoProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial : ViewComponent
    {
        private readonly PortfolyoContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolyoContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var value = _context.HeroSections.FirstOrDefault();
            ViewBag.greeting = value?.Greeting;
            ViewBag.title = value?.Title;
            ViewBag.description = value?.Description;
            ViewBag.mainImageUrl = value?.MainImageUrl;

            // Daktilo (typewriter) efektinde dönecek unvanlar
            ViewBag.heroTitles = _context.HeroTitles
                .Where(x => x.IsActive)
                .OrderBy(x => x.Order)
                .Select(x => x.Title)
                .ToList();
            return View();
        }
    }
}
