using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolyoProject.Context;
using MYAcademy_PortfolyoProject.Entities;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Controllers
{
    // Yetenek grupları (Skill) ve gruplara bağlı yetenekler (SkillItem) yönetimi.
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolyoContext _context;

        public SkillController(PortfolyoContext context)
        {
            _context = context;
        }

        public IActionResult SkillList()
        {
            // Include ile her grubun yetenekleri tek sorguda getirilir.
            var values = _context.Skills
                .Include(x => x.SkillItems)
                .OrderBy(x => x.DisplayOrder)
                .ToList();
            return View(values);
        }

        // ---------------- Yetenek grubu (Skill) ----------------

        [HttpGet]
        public IActionResult CreateSkill()
        {
            return View(new Skill { IconUrl = "bi-code-slash" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
                return View(skill);

            _context.Skills.Add(skill);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek grubu eklendi.";
            return RedirectToAction(nameof(SkillList));
        }

        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Kayıt bulunamadı.";
                return RedirectToAction(nameof(SkillList));
            }
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
                return View(skill);

            _context.Skills.Update(skill);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek grubu güncellendi.";
            return RedirectToAction(nameof(SkillList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value != null)
            {
                // İlişki zorunlu olduğu için gruba bağlı yetenekler de silinir (cascade delete).
                _context.Skills.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Yetenek grubu ve bağlı yetenekleri silindi.";
            }
            return RedirectToAction(nameof(SkillList));
        }

        // ---------------- Yetenek (SkillItem) ----------------

        [HttpGet]
        public IActionResult CreateSkillItem(int? skillId)
        {
            LoadSkillGroups();
            return View(new SkillItem { SkillId = skillId ?? 0, Level = 50 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSkillItem(SkillItem skillItem)
        {
            if (!ModelState.IsValid)
            {
                LoadSkillGroups();
                return View(skillItem);
            }

            _context.SkillItems.Add(skillItem);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek eklendi.";
            return RedirectToAction(nameof(SkillList));
        }

        [HttpGet]
        public IActionResult UpdateSkillItem(int id)
        {
            var value = _context.SkillItems.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Kayıt bulunamadı.";
                return RedirectToAction(nameof(SkillList));
            }
            LoadSkillGroups();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSkillItem(SkillItem skillItem)
        {
            if (!ModelState.IsValid)
            {
                LoadSkillGroups();
                return View(skillItem);
            }

            _context.SkillItems.Update(skillItem);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek güncellendi.";
            return RedirectToAction(nameof(SkillList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSkillItem(int id)
        {
            var value = _context.SkillItems.Find(id);
            if (value != null)
            {
                _context.SkillItems.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Yetenek silindi.";
            }
            return RedirectToAction(nameof(SkillList));
        }

        // Dropdown için grupları veritabanından SelectList olarak hazırlar (asp-items ile kullanılır).
        private void LoadSkillGroups()
        {
            ViewBag.SkillGroups = new SelectList(
                _context.Skills.OrderBy(x => x.DisplayOrder).ToList(),
                nameof(Skill.SkillId),
                nameof(Skill.Title));
        }
    }
}
