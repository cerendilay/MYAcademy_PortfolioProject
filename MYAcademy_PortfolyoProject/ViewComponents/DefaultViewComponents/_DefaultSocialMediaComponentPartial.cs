using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;

namespace MYAcademy_PortfolyoProject.ViewComponents.DefaultViewComponents
{
    // Sosyal medya bağlantıları sitede 3 yerde kullanılır (karşılama, iletişim, footer).
    // "variant" parametresi görünümü belirler: "pill" (yuvarlak ikon) veya "footer" (ikon + ad).
    public class _DefaultSocialMediaComponentPartial : ViewComponent
    {
        private readonly PortfolyoContext _context;

        public _DefaultSocialMediaComponentPartial(PortfolyoContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string variant = "pill")
        {
            ViewBag.Variant = variant;
            var values = _context.SocialMedias
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ToList();
            return View(values);
        }
    }
}
