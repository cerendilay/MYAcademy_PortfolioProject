using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolyoProject.Entities
{
    // Sitedeki "İletişim" bölümünün bilgileri (tek kayıt, sadece güncelleme).
    public class Contact
    {
        public int ContactId { get; set; }

        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(150, ErrorMessage = "Başlık en fazla {1} karakter olabilir.")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla {1} karakter olabilir.")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "E-posta")]
        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Konum")]
        [Required(ErrorMessage = "Konum zorunludur.")]
        [StringLength(100, ErrorMessage = "Konum en fazla {1} karakter olabilir.")]
        public string Location { get; set; } = string.Empty;

        // Boş bırakılırsa sitede telefon gösterilmez.
        [Display(Name = "Telefon")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(30)]
        public string? PhoneNumber { get; set; }
    }
}
