using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;
using MYAcademy_PortfolyoProject.Entities;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
        private readonly PortfolyoContext _context;

        public ExperienceController(PortfolyoContext context)
        {
            _context = context;
        }

        public IActionResult ExperienceList()
        {
            var values = _context.Experiences
                .OrderBy(x => x.DisplayOrder)
                .ThenByDescending(x => x.ExperienceId)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateExperience()
        {
            return View(new Experience());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateExperience(Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return View(experience);
            }

            _context.Experiences.Add(experience);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim eklendi.";
            return RedirectToAction(nameof(ExperienceList));
        }

        [HttpGet]
        public IActionResult UpdateExperience(int id)
        {
            var value = _context.Experiences.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Kayıt bulunamadı.";
                return RedirectToAction(nameof(ExperienceList));
            }
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateExperience(Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return View(experience);
            }

            _context.Experiences.Update(experience);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim güncellendi.";
            return RedirectToAction(nameof(ExperienceList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteExperience(int id)
        {
            var value = _context.Experiences.Find(id);
            if (value != null)
            {
                _context.Experiences.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Deneyim silindi.";
            }
            return RedirectToAction(nameof(ExperienceList));
        }

        // Listeden tek tıkla Aktif/Pasif değiştirme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {
            var value = _context.Experiences.Find(id);
            if (value != null)
            {
                value.IsActive = !value.IsActive;
                _context.SaveChanges();
                TempData["Success"] = value.IsActive
                    ? "Deneyim sitede gösteriliyor."
                    : "Deneyim sitede gizlendi.";
            }
            return RedirectToAction(nameof(ExperienceList));
        }
    }
}
