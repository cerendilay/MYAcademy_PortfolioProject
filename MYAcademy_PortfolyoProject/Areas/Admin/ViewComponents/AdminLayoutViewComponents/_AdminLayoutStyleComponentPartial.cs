using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_PortfolyoProject.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutStyleComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
