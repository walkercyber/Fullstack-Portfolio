
using FullstackLabb3.Data;
using FullstackLabb3.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackLabb3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<SkillService>();
            builder.Services.AddScoped<ProjectService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            //Create
            app.MapPost("/skills", async (Skill skill, SkillService service) =>
            {
                await service.AddSkill(skill);
                return Results.Ok();
            });

            //Get all
            app.MapGet("/skills", async (SkillService service) =>
            {
                var skills = await service.GetSkills();
                return Results.Ok(skills);
            });

            //Get by ID
            app.MapGet("/skills/{id}", async (int id, SkillService service) =>
            {
                var skill = await service.GetSkillById(id);

                if (skill == null)
                {
                    return Results.NotFound("Skill not found");
                }
                return Results.Ok(skill);
            });

            //Update
            app.MapPut("/skills/{id}", async (int id, Skill skill, SkillService service) =>
            {
                var updatedSkill = await service.UpdateSkill(id, skill);
                return updatedSkill != null ? Results.Ok(updatedSkill) : Results.NotFound("Skill not found");
            });

            //Delete
            app.MapDelete("/skills/{id}", async (int id, SkillService service) =>
            {
                var deletedSkill = await service.DeleteSkill(id);
                return deletedSkill != null ? Results.Ok(deletedSkill) : Results.NotFound("Skill not found");
            });

            //Create        
            app.MapPost("/projects", async (Project project, ProjectService service) =>
            {
                await service.AddProject(project);
                return Results.Ok();
            });

            //Get all
            app.MapGet("/projects", async (ProjectService service) =>
            {
                var projects = await service.GetProjects();
                return Results.Ok(projects);
            });

            //Get by ID
            app.MapGet("/projects/{id}", async (int id, ProjectService service) =>
            {
                var project = await service.GetProjectById(id);

                if (project == null)
                {
                    return Results.NotFound("Project not found");
                }
                return Results.Ok(project);
            });

            //Update
            app.MapPut("/projects/{id}", async (int id, Project project, ProjectService service) =>
            {
                var updatedProject = await service.UpdateProject(id, project);
                return updatedProject != null ? Results.Ok(updatedProject) : Results.NotFound("Project not found");
            });

            //Delete
            app.MapDelete("/projects/{id}", async (int id, ProjectService service) =>
            {
                var deletedProject = await service.DeleteProject(id);
                return deletedProject != null ? Results.Ok(deletedProject) : Results.NotFound("Project not found");
            });

            app.Run();
        }
    }
}
