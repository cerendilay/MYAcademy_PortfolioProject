using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;

namespace MYAcademy_PortfolyoProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultAboutComponentPartial : ViewComponent
    {
        private readonly PortfolyoContext _context;

        public _DefaultAboutComponentPartial(PortfolyoContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var value = _context.Abouts.FirstOrDefault();
            return View(value);
        }
    }
}
