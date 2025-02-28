using Microsoft.EntityFrameworkCore;
using PortfolioWebApi.Models;

namespace PortfolioWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
