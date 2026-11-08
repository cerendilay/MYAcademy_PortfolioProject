using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolyoProject.Entities
{
    // Proje kategorisi. Bir kategoride birden çok proje bulunur (bire-çok ilişki).
    public class Category
    {
        public int CategoryId { get; set; }

        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Kategori adı en fazla {1} karakter olabilir.")]
        public string CategoryName { get; set; } = string.Empty;

        public List<Project> Projects { get; set; } = new();
    }
}
