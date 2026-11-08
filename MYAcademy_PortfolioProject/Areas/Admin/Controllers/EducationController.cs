using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult EducationList()
        {
            var values = _context.Educations
                .OrderBy(x => x.DisplayOrder)
                .ThenByDescending(x => x.StartYear)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateEducation()
        {
            return View(new Education());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEducation(Education education)
        {
            ValidateYears(education);
            if (!ModelState.IsValid)
            {
                return View(education);
            }

            _context.Educations.Add(education);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim bilgisi eklendi.";
            return RedirectToAction(nameof(EducationList));
        }

        [HttpGet]
        public IActionResult UpdateEducation(int id)
        {
            var value = _context.Educations.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Kayıt bulunamadı.";
                return RedirectToAction(nameof(EducationList));
            }
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEducation(Education education)
        {
            ValidateYears(education);
            if (!ModelState.IsValid)
            {
                return View(education);
            }

            _context.Educations.Update(education);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim bilgisi güncellendi.";
            return RedirectToAction(nameof(EducationList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEducation(int id)
        {
            var value = _context.Educations.Find(id);
            if (value != null)
            {
                _context.Educations.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Eğitim bilgisi silindi.";
            }
            return RedirectToAction(nameof(EducationList));
        }

        // Bitiş yılı, başlangıç yılından önce olamaz (Data Annotations ile yapılamayan çapraz alan kontrolü).
        private void ValidateYears(Education education)
        {
            if (education.EndYear.HasValue && education.StartYear.HasValue
                && education.EndYear < education.StartYear)
            {
                ModelState.AddModelError(nameof(Education.EndYear), "Bitiş yılı, başlangıç yılından önce olamaz.");
            }
        }
    }
}
