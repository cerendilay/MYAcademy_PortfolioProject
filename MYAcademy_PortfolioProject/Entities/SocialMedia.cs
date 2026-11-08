using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }

        [Display(Name = "Platform")]
        [Required(ErrorMessage = "Platform adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Platform adı en fazla {1} karakter olabilir.")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Bağlantı (URL)")]
        [Required(ErrorMessage = "Bağlantı adresi zorunludur.")]
        [Url(ErrorMessage = "Geçerli bir adres giriniz (https:// ile başlamalı).")]
        [StringLength(300, ErrorMessage = "Adres en fazla {1} karakter olabilir.")]
        public string Url { get; set; } = string.Empty;

        // Bootstrap Icons sınıfı, ör. "bi-github"
        [Display(Name = "İkon")]
        [Required(ErrorMessage = "İkon seçimi zorunludur.")]
        [StringLength(50)]
        public string Icon { get; set; } = string.Empty;

        [Display(Name = "Sıra")]
        [Range(0, 100, ErrorMessage = "Sıra {1} ile {2} arasında olmalıdır.")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Sitede Göster")]
        public bool IsActive { get; set; } = true;
    }
}
