using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolyoProject.Entities
{
    // Yetenek grubu (ör. "Backend & Veritabanı"). Altında birden çok SkillItem bulunur (bire-çok ilişki).
    public class Skill
    {
        public int SkillId { get; set; }

        [Display(Name = "Grup Adı")]
        [Required(ErrorMessage = "Grup adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Grup adı en fazla {1} karakter olabilir.")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(250, ErrorMessage = "Açıklama en fazla {1} karakter olabilir.")]
        public string Description { get; set; } = string.Empty;

        // Bootstrap Icons sınıfı, ör. "bi-server"
        [Display(Name = "İkon")]
        [Required(ErrorMessage = "İkon seçimi zorunludur.")]
        [StringLength(50)]
        public string IconUrl { get; set; } = string.Empty;

        [Display(Name = "Sıra")]
        [Range(0, 100, ErrorMessage = "Sıra {1} ile {2} arasında olmalıdır.")]
        public int DisplayOrder { get; set; }

        public List<SkillItem> SkillItems { get; set; } = new();
    }
}
