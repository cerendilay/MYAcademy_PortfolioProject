using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Context
{
    public class PortfolioContext : DbContext
    {
        // Bağlantı ayarı Program.cs'te appsettings.json'daki "DefaultConnection" ile verilir.
        public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options)
        {
        }

        public DbSet<Experience> Experiences { get; set; }
        public DbSet<HeroSection> HeroSections { get; set; }
        public DbSet<HeroTitle> HeroTitles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillItem> SkillItems { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
    }
}
