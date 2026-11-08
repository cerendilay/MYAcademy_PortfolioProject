using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;
using MYAcademy_PortfolyoProject.Entities;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Controllers
{
    // Sosyal medya bağlantıları. Liste, "İletişim & Sosyal Medya" sayfasında gösterilir.
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly PortfolyoContext _context;

        public SocialMediaController(PortfolyoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            int nextOrder = (_context.SocialMedias.Max(x => (int?)x.DisplayOrder) ?? 0) + 1;
            return View(new SocialMedia { Icon = "bi-globe", DisplayOrder = nextOrder });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSocialMedia(SocialMedia socialMedia)
        {
            if (!ModelState.IsValid)
                return View(socialMedia);

            _context.SocialMedias.Add(socialMedia);
            _context.SaveChanges();
            TempData["Success"] = "Sosyal medya bağlantısı eklendi.";
            return RedirectToAction("ContactList", "Contact");
        }

        [HttpGet]
        public IActionResult UpdateSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Kayıt bulunamadı.";
                return RedirectToAction("ContactList", "Contact");
            }
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSocialMedia(SocialMedia socialMedia)
        {
            if (!ModelState.IsValid)
                return View(socialMedia);

            _context.SocialMedias.Update(socialMedia);
            _context.SaveChanges();
            TempData["Success"] = "Sosyal medya bağlantısı güncellendi.";
            return RedirectToAction("ContactList", "Contact");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSocialMedia(int id)
        {
            var value = _context.SocialMedias.Find(id);
            if (value != null)
            {
                _context.SocialMedias.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Sosyal medya bağlantısı silindi.";
            }
            return RedirectToAction("ContactList", "Contact");
        }
    }
}
