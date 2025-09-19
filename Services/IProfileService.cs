using EmployeeManagement.Models.DTOs.ProfileDTOs;


namespace EmployeeManagement.Services
{
    public interface IProfileService
    {
        Task UpdateProfileAsync(UpdateProfileDto profileDto);
    }
}

