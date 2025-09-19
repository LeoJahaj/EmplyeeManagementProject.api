using EmployeeManagement.Models.DTOs.TaskDTOs;

namespace EmployeeManagement.Services
{
    public interface ITaskService
    {
        Task CreateTaskAsync(CreateTaskDto taskDto);
        Task UpdateTaskAsync(UpdateTaskDto taskDto);
        Task DeleteTaskAsync(int taskId);
        Task<IEnumerable<TaskDto>> GetTasksByProjectAsync(int projectId);
    }
}

