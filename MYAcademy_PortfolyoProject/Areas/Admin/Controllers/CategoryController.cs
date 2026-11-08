using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolyoProject.Context;
using MYAcademy_PortfolyoProject.Entities;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PortfolyoContext _context;

        public CategoryController(PortfolyoContext context)
        {
            _context = context;
        }

        public IActionResult CategoryList()
        {
            // Her kategorideki proje sayısını göstermek için projeler de yüklenir.
            var values = _context.Categories
                .Include(x => x.Projects)
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            CheckDuplicateName(category);
            if (!ModelState.IsValid)
                return View(category);

            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["Success"] = "Kategori eklendi.";
            return RedirectToAction(nameof(CategoryList));
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _context.Categories.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Kayıt bulunamadı.";
                return RedirectToAction(nameof(CategoryList));
            }
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(Category category)
        {
            CheckDuplicateName(category);
            if (!ModelState.IsValid)
                return View(category);

            _context.Categories.Update(category);
            _context.SaveChanges();
            TempData["Success"] = "Kategori güncellendi.";
            return RedirectToAction(nameof(CategoryList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            var value = _context.Categories.Find(id);
            if (value == null)
                return RedirectToAction(nameof(CategoryList));

            // Projeleri olan kategori silinirse projeler de silinirdi; bunun yerine kullanıcı uyarılır.
            int projectCount = _context.Projects.Count(x => x.CategoryId == id);
            if (projectCount > 0)
            {
                TempData["Error"] = $"Bu kategoride {projectCount} proje var. Önce projeleri başka kategoriye taşıyın.";
                return RedirectToAction(nameof(CategoryList));
            }

            _context.Categories.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Kategori silindi.";
            return RedirectToAction(nameof(CategoryList));
        }

        private void CheckDuplicateName(Category category)
        {
            bool exists = _context.Categories.Any(x =>
                x.CategoryName == category.CategoryName && x.CategoryId != category.CategoryId);
            if (exists)
                ModelState.AddModelError(nameof(Category.CategoryName), "Bu isimde bir kategori zaten var.");
        }
    }
}
