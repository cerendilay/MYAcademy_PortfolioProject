namespace MYAcademy_PortfolyoProject.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public List<SkillItem> SkillItems { get; set; }
    }
}
