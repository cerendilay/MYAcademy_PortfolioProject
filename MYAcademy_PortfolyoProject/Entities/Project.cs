using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolyoProject.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Display(Name = "Proje Adı")]
        [Required(ErrorMessage = "Proje adı zorunludur.")]
        [StringLength(150, ErrorMessage = "Proje adı en fazla {1} karakter olabilir.")]
        public string ProjectTitle { get; set; } = string.Empty;

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(1000, ErrorMessage = "Açıklama en fazla {1} karakter olabilir.")]
        public string Description { get; set; } = string.Empty;

        // Virgülle ayrılmış teknoloji listesi: "Flutter, FastAPI, PostgreSQL"
        [Display(Name = "Teknolojiler")]
        [Required(ErrorMessage = "En az bir teknoloji giriniz.")]
        [StringLength(300, ErrorMessage = "Teknolojiler en fazla {1} karakter olabilir.")]
        public string Technologies { get; set; } = string.Empty;

        // Veritabanında yalnızca görselin yolu tutulur (ör. /uploads/projects/abc.jpg)
        [StringLength(300)]
        public string? ImageUrl { get; set; }

        [Display(Name = "GitHub Adresi")]
        [Url(ErrorMessage = "Geçerli bir adres giriniz (https:// ile başlamalı).")]
        [StringLength(300)]
        public string? GithubUrl { get; set; }

        [Display(Name = "Canlı Demo Adresi")]
        [Url(ErrorMessage = "Geçerli bir adres giriniz (https:// ile başlamalı).")]
        [StringLength(300)]
        public string? LiveUrl { get; set; }

        // Projenin yapıldığı tarih; sitede "en yeni" sıralaması bu alana göre yapılır.
        [Display(Name = "Proje Tarihi")]
        [Required(ErrorMessage = "Proje tarihi zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime? ProjectDate { get; set; }

        // Kaydın sisteme eklendiği an (Dashboard'daki "son eklenen projeler" için)
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Pasif projeler admin panelinde görünür ama sitede listelenmez.
        [Display(Name = "Aktif (sitede göster)")]
        public bool IsActive { get; set; } = true;

        // Foreign Key + Navigation Property (Category 1 - N Project)
        [Display(Name = "Kategori")]
        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir kategori seçiniz.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Teknolojileri rozet olarak göstermek için listeye çevirir.
        public IEnumerable<string> GetTechnologyList() =>
            Technologies.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
}
