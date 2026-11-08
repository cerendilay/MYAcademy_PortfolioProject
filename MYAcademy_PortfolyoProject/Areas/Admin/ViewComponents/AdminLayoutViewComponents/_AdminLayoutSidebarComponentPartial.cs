using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;

namespace MYAcademy_PortfolyoProject.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutSidebarComponentPartial : ViewComponent
    {
        private readonly PortfolyoContext _context;

        public _AdminLayoutSidebarComponentPartial(PortfolyoContext context)
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
