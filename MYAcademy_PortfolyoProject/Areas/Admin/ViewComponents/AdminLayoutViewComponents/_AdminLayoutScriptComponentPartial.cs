using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_PortfolyoProject.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
