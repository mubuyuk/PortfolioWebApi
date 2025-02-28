
using Microsoft.EntityFrameworkCore;
using PortfolioWebApi.Data;
using PortfolioWebApi.Models;

namespace PortfolioWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Koppling till SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Hämta alla skills
            app.MapGet("/api/skills", async (AppDbContext db) =>
                await db.Skills.ToListAsync());

            // Hämta en specifik skill via ID
            app.MapGet("/api/skills/{id}", async (int id, AppDbContext db) =>
                await db.Skills.FindAsync(id) is Skill skill ? Results.Ok(skill) : Results.NotFound());

            // Lägg till en ny skill
            app.MapPost("/api/skills", async (Skill skill, AppDbContext db) =>
            {
                db.Skills.Add(skill);
                await db.SaveChangesAsync();
                return Results.Created($"/api/skills/{skill.Id}", skill);
            });

            // Uppdatera en befintlig skill
            app.MapPut("/api/skills/{id}", async (int id, Skill updatedSkill, AppDbContext db) =>
            {
                var skill = await db.Skills.FindAsync(id);
                if (skill == null) return Results.NotFound();

                skill.TechnologyName = updatedSkill.TechnologyName;
                skill.YearsOfExperience = updatedSkill.YearsOfExperience;
                skill.SkillLevel = updatedSkill.SkillLevel;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // Ta bort en skill
            app.MapDelete("/api/skills/{id}", async (int id, AppDbContext db) =>
            {
                var skill = await db.Skills.FindAsync(id);
                if (skill == null) return Results.NotFound();

                db.Skills.Remove(skill);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // Hämta alla projekt
            app.MapGet("/api/projects", async (AppDbContext db) =>
                await db.Projects.ToListAsync());

            // Hämta ett specifikt projekt via ID
            app.MapGet("/api/projects/{id}", async (int id, AppDbContext db) =>
                await db.Projects.FindAsync(id) is Project project ? Results.Ok(project) : Results.NotFound());

            // Lägg till ett nytt projekt
            app.MapPost("/api/projects", async (Project project, AppDbContext db) =>
            {
                db.Projects.Add(project);
                await db.SaveChangesAsync();
                return Results.Created($"/api/projects/{project.Id}", project);
            });

            // Uppdatera ett befintligt projekt
            app.MapPut("/api/projects/{id}", async (int id, Project updatedProject, AppDbContext db) =>
            {
                var project = await db.Projects.FindAsync(id);
                if (project == null) return Results.NotFound();

                project.ProjectName = updatedProject.ProjectName;
                project.Description = updatedProject.Description;
                project.TechnologiesUsed = updatedProject.TechnologiesUsed;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // Ta bort ett projekt
            app.MapDelete("/api/projects/{id}", async (int id, AppDbContext db) =>
            {
                var project = await db.Projects.FindAsync(id);
                if (project == null) return Results.NotFound();

                db.Projects.Remove(project);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.Run();
        }
    }
}
