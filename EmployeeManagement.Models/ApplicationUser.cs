using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string ProfilePicturePath { get; set; } // Store file path or URL
    }
}


