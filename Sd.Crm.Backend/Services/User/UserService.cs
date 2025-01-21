using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Sd.Crm.Backend.Authorization;
using Sd.Crm.Backend.Controllers.Requests.Identity;
using Sd.Crm.Backend.Controllers.Responses.Identity;
using Sd.Crm.Backend.DataLayer;
using Sd.Crm.Backend.Exceptions;
using Sd.Crm.Backend.Model;
using Sd.Crm.Backend.Model.UserModels;

namespace Sd.Crm.Backend.Services.User
{
    public class UserService : IUserService
    {
        private readonly CrmContext _context;
        private readonly PasswordHasher<Model.UserModels.User> _passwordHasher;

        public UserService(CrmContext context, PasswordHasher<Model.UserModels.User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<UserResponse>> GetMentors()
        {
            var mentors = await _context.Users.Where(u => u.Claims != null && u.Claims.Any(c => c.Name == AuthorizationConstants.Role && c.Value == AuthorizationConstants.MentorRole)).ToArrayAsync();
            return mentors.Select(m => m.ToResponse()).ToArray();
        }

        public async Task<IEnumerable<UserResponse>> GetUsers()
        {
            var users = await _context.Users.ToArrayAsync();
            return users.Select(u => u.ToResponse()).ToArray();
        }

        public async Task<UserResponse> Login(RegisterRequest request)
        {
            var user = await _context.Users.Include(u => u.Claims).FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());
            if (user == null)
            {
                throw new NotFoundException($"User {request.Email} not found");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, request.Password);
            if (result != PasswordVerificationResult.Success)
            {
                throw new NotFoundException($"User {request.Email} not found");
            }
            return user.ToResponse();
        }

        public async Task<UserResponse> MakeAdmin(Guid userId)
        {
            var user = await _context.Users.Include(u => u.Claims).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new NotFoundException($"User {userId} not found");
            }

            var roles = user.Claims.Where(c => c.Name == AuthorizationConstants.Role).ToList();
            if (!roles.Any(r => r.Value == AuthorizationConstants.AdminRole))
            {
                var role = new UserClaim(Guid.NewGuid(), AuthorizationConstants.Role, AuthorizationConstants.AdminRole, user);
                user.Claims.Add(role);
                _context.UserClaims.Add(role);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return user.ToResponse();
            }
            else
            {
                throw new AlreadyExistsException($"User {userId} is already a mentor");
            }
        }

        public async Task<UserResponse> MakeMentor(Guid userId)
        {
            var user = await _context.Users.Include(u => u.Claims).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new NotFoundException($"User {userId} not found");
            }

            var roles = user.Claims.Where(c => c.Name == AuthorizationConstants.Role).ToList();
            if (!roles.Any(r => r.Value == AuthorizationConstants.MentorRole))
            {
                var role = new UserClaim(Guid.NewGuid(), AuthorizationConstants.Role, AuthorizationConstants.MentorRole, user);
                user.Claims.Add(role);
                _context.UserClaims.Add(role);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return user.ToResponse();
            }
            else
            {
                throw new AlreadyExistsException($"User {userId} is already a mentor");
            }
        }

        public async Task<UserResponse> Register(RegisterRequest request)
        {
            var existed = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());
            if (existed != null) 
            {
                throw new AlreadyExistsException($"User {request.Email} already exists");
            }

            var user = request.ToEntity();
            user.HashPassword = _passwordHasher.HashPassword(user, request.Password);

            var entry = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return entry.Entity.ToResponse();
        }

        public async Task<UserResponse> Update(Guid id, ProfileUpdateRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User {id} not found");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.City = request.City;
            var entry = _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return entry.Entity.ToResponse();
        }
    }
}
