using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MYAcademy_PortfolyoProject.Context;
using MYAcademy_PortfolyoProject.Entities;

namespace MYAcademy_PortfolyoProject.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolyoContext _context;
        public MessageController(PortfolyoContext context)
        {
            _context = context;
        }   
        public IActionResult MessageList()
        {
            var values= _context.Messages.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public IActionResult UpdateMessage(int id)
        {
            var values = _context.Messages.Find(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdateMessage(Message message)
        {
            _context.Messages.Update(message);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }

        public IActionResult DeleteMessage(int id)
        {
            var values = _context.Messages.Find(id);
            _context.Messages.Remove(values);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }
    }
}
