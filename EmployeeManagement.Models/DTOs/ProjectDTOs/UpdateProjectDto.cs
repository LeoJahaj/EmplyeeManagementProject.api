using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.DTOs.ProjectDTOs
{
    public class UpdateProjectDto
    {
        public int Id { get; set; }           // ID of the project to update
        public string Name { get; set; }      // Updated name of the project
        public string Description { get; set; } // Updated description
        public DateTime StartDate { get; set; } // Updated start date
        public DateTime EndDate { get; set; }   // Updated end date
    }
}
