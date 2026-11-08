using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;

namespace MYAcademy_PortfolyoProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultContactComponentPartial : ViewComponent
    {
        private readonly PortfolyoContext _context;

        public _DefaultContactComponentPartial(PortfolyoContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var value = _context.Contacts.FirstOrDefault();
            return View(value);
        }
    }
}
