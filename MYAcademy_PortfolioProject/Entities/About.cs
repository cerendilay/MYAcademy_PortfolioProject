using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    // Sitede tek kayıt olarak tutulan "Hakkımda" bilgileri (ekle/sil yok, sadece güncelleme).
    public class About
    {
        public int AboutId { get; set; }

        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla {1} karakter olabilir.")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Unvan")]
        [Required(ErrorMessage = "Unvan zorunludur.")]
        [StringLength(100, ErrorMessage = "Unvan en fazla {1} karakter olabilir.")]
        public string JobTitle { get; set; } = string.Empty;

        [Display(Name = "Konum")]
        [StringLength(100, ErrorMessage = "Konum en fazla {1} karakter olabilir.")]
        public string? Location { get; set; }

        [Display(Name = "Bölüm Başlığı")]
        [Required(ErrorMessage = "Bölüm başlığı zorunludur.")]
        [StringLength(150, ErrorMessage = "Başlık en fazla {1} karakter olabilir.")]
        public string SectionTitle { get; set; } = string.Empty;

        // Boş satırla ayrılan her blok sitede ayrı paragraf olarak gösterilir.
        [Display(Name = "Tanıtım Metni")]
        [Required(ErrorMessage = "Tanıtım metni zorunludur.")]
        [StringLength(2000, ErrorMessage = "Tanıtım metni en fazla {1} karakter olabilir.")]
        public string Description { get; set; } = string.Empty;

        // Yüklenen fotoğrafın yolu (ör. /uploads/about/abc.jpg). Dosyanın kendisi wwwroot'ta durur.
        [StringLength(300)]
        public string? ImageUrl { get; set; }

        // Öne çıkan bilgiler (ör. "4+" / "Uçtan Uca Proje")
        [StringLength(20)] public string? Stat1Value { get; set; }
        [StringLength(40)] public string? Stat1Label { get; set; }
        [StringLength(20)] public string? Stat2Value { get; set; }
        [StringLength(40)] public string? Stat2Label { get; set; }
        [StringLength(20)] public string? Stat3Value { get; set; }
        [StringLength(40)] public string? Stat3Label { get; set; }
    }
}
