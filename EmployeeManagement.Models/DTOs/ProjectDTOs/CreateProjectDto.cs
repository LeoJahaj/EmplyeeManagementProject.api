using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.DTOs.ProjectDTOs
{
    public class CreateProjectDto
    {
        public string Name { get; set; }       // Name of the project
        public string Description { get; set; } // Description of the project
        public DateTime StartDate { get; set; } // Start date of the project
        public DateTime EndDate { get; set; }   // End date of the project
    }
}
