using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolyoProject.Context;
using MYAcademy_PortfolyoProject.Entities;
using MYAcademy_PortfolyoProject.Services;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private const string ImageFolder = "projects";

        private readonly PortfolyoContext _context;
        private readonly ImageUploadService _imageUploadService;

        public ProjectController(PortfolyoContext context, ImageUploadService imageUploadService)
        {
            _context = context;
            _imageUploadService = imageUploadService;
        }

        // Kategori filtresi, durum filtresi ve sıralama sorgu parametreleriyle (query string) yapılır.
        public IActionResult ProjectList(int? categoryId, string? status, string sort = "newest")
        {
            IQueryable<Project> query = _context.Projects.Include(x => x.Category);

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId);

            if (status == "active")
                query = query.Where(x => x.IsActive);
            else if (status == "passive")
                query = query.Where(x => !x.IsActive);

            query = sort switch
            {
                "oldest" => query.OrderBy(x => x.ProjectDate),
                "title" => query.OrderBy(x => x.ProjectTitle),
                "recentlyAdded" => query.OrderByDescending(x => x.CreatedDate),
                _ => query.OrderByDescending(x => x.ProjectDate) // newest
            };

            ViewBag.Categories = new SelectList(_context.Categories.OrderBy(x => x.CategoryName).ToList(),
                nameof(Category.CategoryId), nameof(Category.CategoryName), categoryId);
            ViewBag.SelectedStatus = status;
            ViewBag.SelectedSort = sort;
            ViewBag.TotalCount = _context.Projects.Count();
            ViewBag.ActiveCount = _context.Projects.Count(x => x.IsActive);

            return View(query.ToList());
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            LoadCategories();
            return View(new Project { ProjectDate = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject(Project project, IFormFile? imageFile)
        {
            if (imageFile == null)
                ModelState.AddModelError("imageFile", "Proje görseli zorunludur.");

            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(project);
            }

            var (path, error) = await _imageUploadService.SaveAsync(imageFile!, ImageFolder);
            if (error != null)
            {
                ModelState.AddModelError("imageFile", error);
                LoadCategories();
                return View(project);
            }

            project.ImageUrl = path;
            project.CreatedDate = DateTime.Now;
            _context.Projects.Add(project);
            _context.SaveChanges();
            TempData["Success"] = "Proje eklendi.";
            return RedirectToAction(nameof(ProjectList));
        }

        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Kayıt bulunamadı.";
                return RedirectToAction(nameof(ProjectList));
            }
            LoadCategories();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProject(Project project, IFormFile? imageFile)
        {
            // Mevcut görsel yolu ve eklenme tarihi veritabanından alınır (formdan gelen değerlere güvenilmez).
            var existing = _context.Projects.AsNoTracking().FirstOrDefault(x => x.ProjectId == project.ProjectId);
            if (existing == null)
            {
                TempData["Error"] = "Kayıt bulunamadı.";
                return RedirectToAction(nameof(ProjectList));
            }
            project.ImageUrl = existing.ImageUrl;
            project.CreatedDate = existing.CreatedDate;

            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(project);
            }

            // Yeni görsel seçildiyse yüklenir, eskisi silinir; seçilmediyse eski görsel korunur.
            if (imageFile != null)
            {
                var (path, error) = await _imageUploadService.SaveAsync(imageFile, ImageFolder);
                if (error != null)
                {
                    ModelState.AddModelError("imageFile", error);
                    LoadCategories();
                    return View(project);
                }
                project.ImageUrl = path;
                _imageUploadService.Delete(existing.ImageUrl);
            }

            _context.Projects.Update(project);
            _context.SaveChanges();
            TempData["Success"] = "Proje güncellendi.";
            return RedirectToAction(nameof(ProjectList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value != null)
            {
                _imageUploadService.Delete(value.ImageUrl);
                _context.Projects.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Proje silindi.";
            }
            return RedirectToAction(nameof(ProjectList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {
            var value = _context.Projects.Find(id);
            if (value != null)
            {
                value.IsActive = !value.IsActive;
                _context.SaveChanges();
                TempData["Success"] = value.IsActive ? "Proje sitede gösteriliyor." : "Proje pasife alındı, sitede gizlendi.";
            }
            // Filtreler kaybolmasın diye kullanıcı geldiği listeye döner.
            var referer = Request.Headers.Referer.ToString();
            return !string.IsNullOrEmpty(referer) && Url.IsLocalUrl(new Uri(referer).PathAndQuery)
                ? Redirect(new Uri(referer).PathAndQuery)
                : RedirectToAction(nameof(ProjectList));
        }

        // Kategori dropdown'u veritabanından doldurulur (SelectList + asp-items).
        private void LoadCategories()
        {
            ViewBag.Categories = new SelectList(
                _context.Categories.OrderBy(x => x.CategoryName).ToList(),
                nameof(Category.CategoryId),
                nameof(Category.CategoryName));
        }
    }
}
