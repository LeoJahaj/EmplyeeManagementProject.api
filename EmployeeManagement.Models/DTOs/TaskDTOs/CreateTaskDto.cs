using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.DTOs.TaskDTOs
{
    public class CreateTaskDto
    {
        public int Id { get; set; }             // Task ID to identify which task to update
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedToId { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }
        
    }
}
