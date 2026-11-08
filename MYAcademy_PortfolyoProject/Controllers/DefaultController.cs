using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolyoProject.Context;
using MYAcademy_PortfolyoProject.Entities;

namespace MYAcademy_PortfolyoProject.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolyoContext _context;

        public DefaultController(PortfolyoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Sitedeki iletişim formu bu aksiyona fetch (AJAX) ile gönderilir; sayfa yenilenmez.
        // [Bind] ile yalnızca formdaki alanlar alınır; IsRead/SendDate dışarıdan değiştirilemez.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(
            [Bind(nameof(Message.SenderName), nameof(Message.SenderEmail),
                  nameof(Message.Subject), nameof(Message.MessageDetail))] Message message,
            string? website)
        {
            // Bal küpü (honeypot): gerçek kullanıcı bu gizli alanı görmez; doluysa bot kabul edilir.
            if (!string.IsNullOrEmpty(website))
            {
                return Json(new { success = true, message = "Mesajınız alındı." });
            }

            if (!ModelState.IsValid)
            {
                // Alan bazlı hata mesajları: { "SenderEmail": "Geçerli bir e-posta adresi giriniz.", ... }
                var errors = ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value!.Errors.First().ErrorMessage);
                return BadRequest(new { success = false, errors });
            }

            message.SenderName = message.SenderName.Trim();
            message.SenderEmail = message.SenderEmail.Trim();
            message.Subject = message.Subject.Trim();
            message.MessageDetail = message.MessageDetail.Trim();
            message.SendDate = DateTime.Now;
            message.IsRead = false;

            _context.Messages.Add(message);
            _context.SaveChanges();

            return Json(new { success = true, message = "Teşekkürler! Mesajınız bana ulaştı, en kısa sürede dönüş yapacağım." });
        }
    }
}
