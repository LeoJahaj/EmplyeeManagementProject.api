using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Models.DTOs.ProjectDTOs;

namespace EmployeeManagement.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateProjectAsync(CreateProjectDto projectDto)
        {
            var project = new ProjectEntity
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(UpdateProjectDto projectDto)
        {
            var project = await _context.Projects.FindAsync(projectDto.Id);
            if (project != null)
            {
                project.Name = projectDto.Name;
                project.Description = projectDto.Description;
                project.StartDate = projectDto.StartDate;
                project.EndDate = projectDto.EndDate;

                _context.Projects.Update(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .Select(project => new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description
                })
                .ToListAsync();
        }
    }
}

