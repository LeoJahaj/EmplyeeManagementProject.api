using EmployeeManagement.Models.DTOs.ProjectDTOs;

namespace EmployeeManagement.Services
{
    public interface IProjectService
    {
        Task CreateProjectAsync(CreateProjectDto projectDto);
        Task UpdateProjectAsync(UpdateProjectDto projectDto);
        Task DeleteProjectAsync(int projectId);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    }
}
