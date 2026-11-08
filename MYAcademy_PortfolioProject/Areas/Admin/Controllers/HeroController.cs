using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    // Ana sayfa karşılama alanı (HeroSection) ve daktilo unvanları (HeroTitle) yönetimi.
    [Area("Admin")]
    public class HeroController : Controller
    {
        private readonly PortfolioContext _context;

        public HeroController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var hero = _context.HeroSections.FirstOrDefault() ?? new HeroSection();
            LoadHeroTitles();
            return View(hero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(HeroSection hero)
        {
            if (!ModelState.IsValid)
            {
                LoadHeroTitles();
                return View(hero);
            }

            // Tek kayıt mantığı: kayıt yoksa eklenir, varsa güncellenir.
            if (hero.HeroSectionId == 0)
                _context.HeroSections.Add(hero);
            else
                _context.HeroSections.Update(hero);

            _context.SaveChanges();
            TempData["Success"] = "Ana sayfa bilgileri güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddHeroTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length > 60)
            {
                TempData["Error"] = "Unvan boş olamaz ve en fazla 60 karakter olabilir.";
                return RedirectToAction(nameof(Index));
            }

            int nextOrder = (_context.HeroTitles.Max(x => (int?)x.Order) ?? 0) + 1;
            _context.HeroTitles.Add(new HeroTitle { Title = title.Trim(), Order = nextOrder, IsActive = true });
            _context.SaveChanges();
            TempData["Success"] = "Unvan eklendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleHeroTitle(int id)
        {
            var value = _context.HeroTitles.Find(id);
            if (value != null)
            {
                value.IsActive = !value.IsActive;
                _context.SaveChanges();
                TempData["Success"] = value.IsActive ? "Unvan sitede gösteriliyor." : "Unvan gizlendi.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteHeroTitle(int id)
        {
            var value = _context.HeroTitles.Find(id);
            if (value != null)
            {
                _context.HeroTitles.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Unvan silindi.";
            }
            return RedirectToAction(nameof(Index));
        }

        private void LoadHeroTitles()
        {
            ViewBag.HeroTitles = _context.HeroTitles.OrderBy(x => x.Order).ToList();
        }
    }
}
