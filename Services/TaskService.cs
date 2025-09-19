using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models.DTOs.TaskDTOs;


namespace EmployeeManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateTaskAsync(CreateTaskDto taskDto)
        {
            var task = new TaskEntity
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                AssignedToId = taskDto.AssignedToId,  // Assuming the property name is 'AssignedUserId' in DTO
                Status = taskDto.Status,
                ProjectId = taskDto.ProjectId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(UpdateTaskDto taskDto)
        {
            var task = await _context.Tasks.FindAsync(taskDto.Id);
            if (task != null)
            {
                // Only update properties if the new value is not null
                task.Title = taskDto.Title ?? task.Title;  // Update only if not null
                task.Description = taskDto.Description ?? task.Description;
                task.AssignedToId = taskDto.AssignedToId ?? task.AssignedToId;
                task.Status = taskDto.Status ?? task.Status;

                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByProjectAsync(int projectId)
        {
            return await _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .Select(task => new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Status = task.Status
                })
                .ToListAsync();
        }
    }
}
