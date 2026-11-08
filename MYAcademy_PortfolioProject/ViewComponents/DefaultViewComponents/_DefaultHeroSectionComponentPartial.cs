using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var value = _context.HeroSections.FirstOrDefault();
            ViewBag.greeting = value?.Greeting;
            ViewBag.title = value?.Title;
            ViewBag.description = value?.Description;
            // Fotoğraf ve ad, "Hakkımda" kaydından gelir (tek yerden yönetim).
            var about = _context.Abouts.FirstOrDefault();
            ViewBag.imageUrl = about?.ImageUrl;
            ViewBag.fullName = about?.FullName;

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
