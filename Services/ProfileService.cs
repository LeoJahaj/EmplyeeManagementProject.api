using EmployeeManagement.Models;
using System.Threading.Tasks;
using EmployeeManagement.Models.DTOs.ProfileDTOs;


namespace EmployeeManagement.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _context;

        public ProfileService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpdateProfileAsync(UpdateProfileDto profileDto)
        {
            var user = await _context.Users.FindAsync(profileDto.UserId);
            if (user != null)
            {
                user.FullName = profileDto.FullName;
                user.Email = profileDto.Email;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        

        public async Task UpdateProfilePictureAsync(int userId, IFormFile profilePicture)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                if (profilePicture != null)
                {
                    // Save the profile picture to a directory and get the file path
                    var filePath = Path.Combine("wwwroot", "uploads", "profile_pictures", profilePicture.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await profilePicture.CopyToAsync(fileStream);
                    }

                    // Update the user's profile picture path in the database
                    user.ProfilePicturePath = filePath;

                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
            }
        }

    }
}

//public async Task UpdateProfilePictureAsync(int userId, byte[] profilePicture)
        //{
        //    var user = await _context.Users.FindAsync(userId);
        //    if (user != null)
        //    {
        //        user.ProfilePicture = profilePicture;

        //        _context.Users.Update(user);
        //        await _context.SaveChangesAsync();
        //    }
        //}