using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Experience
    {
        public int ExperienceId { get; set; }

        [Display(Name = "Pozisyon / Unvan")]
        [Required(ErrorMessage = "Pozisyon veya unvan zorunludur.")]
        [StringLength(150, ErrorMessage = "Unvan en fazla {1} karakter olabilir.")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Kurum / Şirket")]
        [Required(ErrorMessage = "Kurum veya şirket adı zorunludur.")]
        [StringLength(150, ErrorMessage = "Kurum adı en fazla {1} karakter olabilir.")]
        public string CompanyName { get; set; } = string.Empty;

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(1000, ErrorMessage = "Açıklama en fazla {1} karakter olabilir.")]
        public string Description { get; set; } = string.Empty;

        // Sitede rozet olarak gösterilen tür bilgisi (Staj, Tam Zamanlı, Gönüllü...)
        [Display(Name = "Tür")]
        [Required(ErrorMessage = "Deneyim türü zorunludur (ör. Staj).")]
        [StringLength(50, ErrorMessage = "Tür en fazla {1} karakter olabilir.")]
        public string CategoryName { get; set; } = string.Empty;

        // Serbest metin tarih aralığı: "Tem 2025 – Eyl 2025", "Ocak 2026 – Devam ediyor"
        [Display(Name = "Tarih Aralığı")]
        [StringLength(60, ErrorMessage = "Tarih aralığı en fazla {1} karakter olabilir.")]
        public string? Period { get; set; }

        [Display(Name = "Sıra")]
        [Range(0, 100, ErrorMessage = "Sıra {1} ile {2} arasında olmalıdır.")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Sitede Göster")]
        public bool IsActive { get; set; } = true;
    }
}
