using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models.DTOs.ProfileDTOs
{
    public class UpdateProfileDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProfilePicturePath { get; set; } // This will store the file path to the image
    }
}
