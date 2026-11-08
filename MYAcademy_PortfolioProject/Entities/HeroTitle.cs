using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    // Ana sayfadaki daktilo efektinde sırayla yazılan unvanlar.
    public class HeroTitle
    {
        public int HeroTitleId { get; set; }

        [Display(Name = "Unvan")]
        [Required(ErrorMessage = "Unvan zorunludur.")]
        [StringLength(60, ErrorMessage = "Unvan en fazla {1} karakter olabilir.")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Sıra")]
        [Range(0, 100, ErrorMessage = "Sıra {1} ile {2} arasında olmalıdır.")]
        public int Order { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
