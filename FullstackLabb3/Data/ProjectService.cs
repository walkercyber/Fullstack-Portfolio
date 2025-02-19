using FullstackLabb3.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackLabb3.Data
    {
        public class ProjectService
        {
            private readonly DataContext _db;

            public ProjectService(DataContext context)
            {
                _db = context;
            }

            public async Task AddProject(Project project)
            {
                await _db.Projects.AddAsync(project);
                await _db.SaveChangesAsync();
            }
             public async Task<Project> GetProjectById(int id)
             {
            return await _db.Projects.FirstOrDefaultAsync(x => x.Id == id);
             }

        public async Task<List<Project>> GetProjects()
            {
                return await _db.Projects.ToListAsync();
            }

            public async Task<Project> GetProject(int id)
            {
                return await _db.Projects.FirstOrDefaultAsync(x => x.Id == id);
            }

            public async Task<Project> UpdateProject(int id, Project updatedProject)
            {
                var project = await _db.Projects.FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                    return null;

                project.Title = updatedProject.Title;
                project.Description = updatedProject.Description;
                project.TechnologiesUsed = updatedProject.TechnologiesUsed;

                await _db.SaveChangesAsync();
                return project;
            }

            public async Task<Project> DeleteProject(int id)
            {
                var project = await _db.Projects.FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                    return null;

                _db.Projects.Remove(project);
                await _db.SaveChangesAsync();
                return project;
            }
        }
    }

