using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    // Sitedeki iletişim formundan gönderilen mesaj.
    // Admin panelinde içerik düzenlenmez; sadece okundu/okunmadı durumu yönetilir.
    public class Message
    {
        public int MessageId { get; set; }

        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Lütfen adınızı ve soyadınızı yazın.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad soyad {2} ile {1} karakter arasında olmalıdır.")]
        public string SenderName { get; set; } = string.Empty;

        [Display(Name = "E-posta")]
        [Required(ErrorMessage = "Lütfen e-posta adresinizi yazın.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(150, ErrorMessage = "E-posta en fazla {1} karakter olabilir.")]
        public string SenderEmail { get; set; } = string.Empty;

        [Display(Name = "Konu")]
        [Required(ErrorMessage = "Lütfen mesajınızın konusunu yazın.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Konu {2} ile {1} karakter arasında olmalıdır.")]
        public string Subject { get; set; } = string.Empty;

        [Display(Name = "Mesaj")]
        [Required(ErrorMessage = "Lütfen mesajınızı yazın.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Mesaj {2} ile {1} karakter arasında olmalıdır.")]
        public string MessageDetail { get; set; } = string.Empty;

        public DateTime SendDate { get; set; }

        public bool IsRead { get; set; }
    }
}
