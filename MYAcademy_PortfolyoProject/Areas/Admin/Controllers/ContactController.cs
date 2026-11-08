using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;
using MYAcademy_PortfolyoProject.Entities;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Controllers
{
    // İletişim bilgileri tek kayıttır (güncelleme odaklı). Aynı sayfada sosyal medya listesi de gösterilir.
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly PortfolyoContext _context;

        public ContactController(PortfolyoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ContactList()
        {
            var value = _context.Contacts.FirstOrDefault() ?? new Contact();
            LoadSocialMedias();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ContactList(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                LoadSocialMedias();
                return View(contact);
            }

            if (contact.ContactId == 0)
                _context.Contacts.Add(contact);
            else
                _context.Contacts.Update(contact);

            _context.SaveChanges();
            TempData["Success"] = "İletişim bilgileri güncellendi.";
            return RedirectToAction(nameof(ContactList));
        }

        private void LoadSocialMedias()
        {
            ViewBag.SocialMedias = _context.SocialMedias.OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
