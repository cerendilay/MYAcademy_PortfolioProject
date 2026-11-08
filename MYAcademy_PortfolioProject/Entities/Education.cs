using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Education
    {
        public int EducationId { get; set; }

        [Display(Name = "Bölüm / Program")]
        [Required(ErrorMessage = "Bölüm veya program adı zorunludur.")]
        [StringLength(150, ErrorMessage = "Bölüm adı en fazla {1} karakter olabilir.")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Okul / Kurum")]
        [Required(ErrorMessage = "Okul veya kurum adı zorunludur.")]
        [StringLength(150, ErrorMessage = "Okul adı en fazla {1} karakter olabilir.")]
        public string SchoolName { get; set; } = string.Empty;

        [Display(Name = "Derece")]
        [Required(ErrorMessage = "Derece bilgisi zorunludur (ör. Lisans).")]
        [StringLength(50, ErrorMessage = "Derece en fazla {1} karakter olabilir.")]
        public string Degree { get; set; } = string.Empty;

        [Display(Name = "Açıklama")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla {1} karakter olabilir.")]
        public string? Description { get; set; }

        [Display(Name = "Başlangıç Yılı")]
        [Required(ErrorMessage = "Başlangıç yılı zorunludur.")]
        [Range(1950, 2100, ErrorMessage = "Başlangıç yılı {1} ile {2} arasında olmalıdır.")]
        public int? StartYear { get; set; }

        // Boş bırakılırsa "Devam Ediyor" kabul edilir.
        [Display(Name = "Bitiş Yılı")]
        [Range(1950, 2100, ErrorMessage = "Bitiş yılı {1} ile {2} arasında olmalıdır.")]
        public int? EndYear { get; set; }

        // Sitede gösterim sırası (küçük olan önce)
        [Display(Name = "Sıra")]
        [Range(0, 100, ErrorMessage = "Sıra {1} ile {2} arasında olmalıdır.")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Sitede Göster")]
        public bool IsActive { get; set; } = true;
    }
}
