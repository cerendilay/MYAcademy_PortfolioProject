using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Controllers
{
    // Gelen kutusu: mesaj içeriği düzenlenmez, yalnızca okundu/okunmadı durumu yönetilir.
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolyoContext _context;

        public MessageController(PortfolyoContext context)
        {
            _context = context;
        }

        // En yeniden eskiye liste + gönderen/konu/e-posta araması + okundu filtresi
        public IActionResult MessageList(string? search, string? filter)
        {
            var query = _context.Messages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(x => x.SenderName.Contains(search)
                                      || x.Subject.Contains(search)
                                      || x.SenderEmail.Contains(search));
            }

            if (filter == "unread")
                query = query.Where(x => !x.IsRead);
            else if (filter == "read")
                query = query.Where(x => x.IsRead);

            ViewBag.Search = search;
            ViewBag.Filter = filter;
            ViewBag.TotalCount = _context.Messages.Count();
            ViewBag.UnreadCount = _context.Messages.Count(x => !x.IsRead);

            return View(query.OrderByDescending(x => x.SendDate).ToList());
        }

        // Modal (popup) için mesaj detayını JSON olarak döner; sayfa yenilenmez.
        [HttpGet]
        public IActionResult GetMessage(int id)
        {
            var value = _context.Messages.Find(id);
            if (value == null)
                return NotFound();

            return Json(new
            {
                value.MessageId,
                value.SenderName,
                value.SenderEmail,
                value.Subject,
                value.MessageDetail,
                SendDate = value.SendDate.ToString("dd MMMM yyyy, HH:mm"),
                value.IsRead
            });
        }

        // Okundu / okunmadı durumunu değiştirir. Hem fetch (JSON) hem normal form ile kullanılabilir.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetReadStatus(int id, bool isRead)
        {
            var value = _context.Messages.Find(id);
            if (value == null)
                return NotFound();

            value.IsRead = isRead;
            _context.SaveChanges();

            return Json(new { success = true, isRead, unreadCount = _context.Messages.Count(x => !x.IsRead) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAllAsRead()
        {
            var unread = _context.Messages.Where(x => !x.IsRead).ToList();
            unread.ForEach(x => x.IsRead = true);
            _context.SaveChanges();
            TempData["Success"] = $"{unread.Count} mesaj okundu olarak işaretlendi.";
            return RedirectToAction(nameof(MessageList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                _context.Messages.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Mesaj silindi.";
            }
            return RedirectToAction(nameof(MessageList));
        }
    }
}
