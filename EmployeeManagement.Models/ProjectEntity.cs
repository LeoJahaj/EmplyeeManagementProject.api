using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class ProjectEntity
    {
        public int Id { get; set; }            // Unique ID for the project
        public string Name { get; set; }       // Name of the project
        public string Description { get; set; } // Description of the project
        public DateTime StartDate { get; set; } // Start date of the project
        public DateTime EndDate { get; set; }   // End date of the project
    }
}
