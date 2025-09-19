using EmployeeManagement.Models.DTOs.UserDTOs;

namespace EmployeeManagement.Services
{
    // The IUserService interface will define user-related operations
    public interface IUserService
    {
        // Method to create a new user. It accepts a CreateUserDto object.
        Task CreateUserAsync(CreateUserDto userDto);

        // Method to update an existing user. It accepts an UpdateUserDto object.
        Task UpdateUserAsync(UpdateUserDto userDto);

        // Method to delete a user by their ID.
        Task DeleteUserAsync(String userId);

        // Method to get a user by their ID. Returns a UserDto object.
        Task<UserDto> GetUserByIdAsync(String userId);
    }
}

