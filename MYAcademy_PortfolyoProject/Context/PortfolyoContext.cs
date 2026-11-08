using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolyoProject.Entities;

namespace MYAcademy_PortfolyoProject.Context
{
    public class PortfolyoContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=PortfolyoDb;Integrated Security=True;TrustServerCertificate=True");
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
    }
}
