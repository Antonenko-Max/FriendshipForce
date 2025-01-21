using Microsoft.AspNetCore.Identity.Data;
using Sd.Crm.Backend.Controllers.Requests.Identity;
using Sd.Crm.Backend.Controllers.Responses.Identity;

namespace Sd.Crm.Backend.Services.User
{
    public interface IUserService
    {
        Task<UserResponse> Register(RegisterRequest request);
        Task<UserResponse> Login(RegisterRequest request);
        Task<UserResponse> Update(Guid id, ProfileUpdateRequest request);
        Task<IEnumerable<UserResponse>> GetMentors();
        Task<IEnumerable<UserResponse>> GetUsers();
        Task<UserResponse> MakeMentor(Guid userId);
        Task<UserResponse> MakeAdmin(Guid userId);
    }
}
