using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutSidebarComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _AdminLayoutSidebarComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Sidebar'daki "Gelen Kutusu" rozetinde okunmamış mesaj sayısı gösterilir.
            ViewBag.UnreadMessageCount = _context.Messages.Count(x => !x.IsRead);

            // Aktif menüyü işaretlemek için o an çalışan controller adı alınır.
            ViewBag.ActiveController = RouteData.Values["controller"]?.ToString();
            return View();
        }
    }
}
