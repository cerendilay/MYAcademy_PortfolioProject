using MYAcademy_PortfolyoProject.Entities;

namespace MYAcademy_PortfolyoProject.Areas.Admin.Models
{
    // Dashboard'da farklı tablolardan gelen verileri tek bir modelde toplar.
    public class DashboardViewModel
    {
        // İstatistik kartları
        public int TotalProjectCount { get; set; }
        public int ActiveProjectCount { get; set; }
        public int SkillCount { get; set; }
        public int ExperienceCount { get; set; }
        public int EducationCount { get; set; }
        public int TotalMessageCount { get; set; }
        public int UnreadMessageCount { get; set; }

        // Listeler
        public List<Message> LastMessages { get; set; } = new();
        public List<Project> LastProjects { get; set; } = new();
        public List<CategorySummary> CategorySummaries { get; set; } = new();
    }

    // GroupBy sorgusunun sonucu: kategori başına proje sayısı
    public class CategorySummary
    {
        public string CategoryName { get; set; } = string.Empty;
        public int ProjectCount { get; set; }
        public int ActiveProjectCount { get; set; }
    }
}
