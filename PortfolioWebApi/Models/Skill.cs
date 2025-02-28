namespace PortfolioWebApi.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string TechnologyName { get; set; } = string.Empty;
        public string YearsOfExperience { get; set; } = string.Empty;
        public int SkillLevel { get; set; }

    }
}
