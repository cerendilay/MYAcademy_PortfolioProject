using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;
using MYAcademy_PortfolioProject.Services;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    // "Hakkımda" tek kayıttır: ekleme/silme yerine güncelleme odaklı yönetilir.
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly ImageUploadService _imageUploadService;

        public AboutController(PortfolioContext context, ImageUploadService imageUploadService)
        {
            _context = context;
            _imageUploadService = imageUploadService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = _context.Abouts.FirstOrDefault() ?? new About();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(About about, IFormFile? imageFile, bool removeImage = false)
        {
            // Mevcut fotoğraf yolu formdan değil veritabanından okunur (formdaki gizli alan değiştirilebilir).
            string? oldImage = _context.Abouts
                .Where(x => x.AboutId == about.AboutId)
                .Select(x => x.ImageUrl)
                .FirstOrDefault();
            about.ImageUrl = oldImage;

            if (!ModelState.IsValid)
            {
                return View(about);
            }

            if (imageFile != null)
            {
                var (path, error) = await _imageUploadService.SaveAsync(imageFile, "about");
                if (error != null)
                {
                    ModelState.AddModelError("imageFile", error);
                    return View(about);
                }
                about.ImageUrl = path;
                _imageUploadService.Delete(oldImage);
            }
            else if (removeImage)
            {
                about.ImageUrl = null;
                _imageUploadService.Delete(oldImage);
            }

            if (about.AboutId == 0)
                _context.Abouts.Add(about);
            else
                _context.Abouts.Update(about);

            _context.SaveChanges();
            TempData["Success"] = "Hakkımda bilgileri güncellendi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
