using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.DTOs.TaskDTOs
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }             // Task ID to identify which task to update
        public string Title { get; set; }        // Title of the task
        public string Description { get; set; }  // Description of the task
        public string AssignedToId { get; set; } // User to whom the task is assigned (could be null if not assigned)
        public string Status { get; set; }       // Status (e.g., Pending, In Progress, Completed)
        public int ProjectId { get; set; }      // Optional, to update the associated project
        
    }
}
