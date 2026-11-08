using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    // Tek bir yetenek (ör. "C#" - %80). Bir Skill grubuna bağlıdır.
    public class SkillItem
    {
        public int SkillItemId { get; set; }

        [Display(Name = "Yetenek Adı")]
        [Required(ErrorMessage = "Yetenek adı zorunludur.")]
        [StringLength(60, ErrorMessage = "Yetenek adı en fazla {1} karakter olabilir.")]
        public string SkillItemTitle { get; set; } = string.Empty;

        [Display(Name = "Seviye (%)")]
        [Range(0, 100, ErrorMessage = "Seviye 0 ile 100 arasında olmalıdır.")]
        public int Level { get; set; }

        // Foreign Key + Navigation Property
        [Display(Name = "Yetenek Grubu")]
        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir yetenek grubu seçiniz.")]
        public int SkillId { get; set; }
        public Skill? Skill { get; set; }
    }
}
