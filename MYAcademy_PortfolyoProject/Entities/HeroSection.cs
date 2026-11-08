using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolyoProject.Entities
{
    // Ana sayfanın en üstündeki karşılama alanı (tek kayıt, sadece güncelleme).
    public class HeroSection
    {
        public int HeroSectionId { get; set; }

        [Display(Name = "Karşılama Metni")]
        [Required(ErrorMessage = "Karşılama metni zorunludur.")]
        [StringLength(100, ErrorMessage = "Karşılama metni en fazla {1} karakter olabilir.")]
        public string Greeting { get; set; } = string.Empty;

        [Display(Name = "Ana Başlık")]
        [Required(ErrorMessage = "Ana başlık zorunludur.")]
        [StringLength(100, ErrorMessage = "Ana başlık en fazla {1} karakter olabilir.")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Kısa Tanıtım")]
        [Required(ErrorMessage = "Kısa tanıtım zorunludur.")]
        [StringLength(500, ErrorMessage = "Kısa tanıtım en fazla {1} karakter olabilir.")]
        public string Description { get; set; } = string.Empty;

        // Fotoğraf artık "Hakkımda" kaydından gelir; bu alan geriye dönük uyumluluk için duruyor.
        public string? MainImageUrl { get; set; }
    }
}
