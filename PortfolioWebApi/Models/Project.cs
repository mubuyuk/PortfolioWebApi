namespace PortfolioWebApi.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? TechnologiesUsed { get; set; }
    }
}
