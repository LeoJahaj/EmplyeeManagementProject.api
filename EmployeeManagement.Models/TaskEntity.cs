using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class TaskEntity  // Rename it to a more descriptive name if needed
    {
        public int Id { get; set; }            // Primary Key
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string AssignedToId { get; set; }  // Assuming a reference to a user
        public int ProjectId { get; set; }      // Foreign key for the project
    }
}
