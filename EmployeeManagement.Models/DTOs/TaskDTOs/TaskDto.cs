using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.DTOs.TaskDTOs
{
   
     public class TaskDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string AssignedToId { get; set; }
            public string Status { get; set; }
            public int ProjectId { get; set; }
            
     }
   

}
