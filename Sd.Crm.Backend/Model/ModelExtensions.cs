using Microsoft.AspNetCore.Identity.Data;
using Sd.Crm.Backend.Controllers.Requests.Squad;
using Sd.Crm.Backend.Controllers.Responses.Identity;
using Sd.Crm.Backend.Model.SquadModels;
using Sd.Crm.Backend.Model.UserModels;

namespace Sd.Crm.Backend.Model
{
    public static class ModelExtensions
    {
        public static User ToEntity(this RegisterRequest request) =>
            new User() { Id = Guid.NewGuid(), Email = request.Email };

        public static UserResponse ToResponse(this User user) =>
            new UserResponse() 
            { 
                Id = user.Id, 
                Email = user.Email, 
                FirstName = user.FirstName, 
                LastName = user.LastName, 
                City = user.City,
                Claims = user.Claims?.Select(c => new ClaimResponse(c.Name, c.Value)).ToList()
            };

        public static Disciple ToEntity(this DiscipleRequest request) =>
            new Disciple()
            {
                Id = request.Id ?? Guid.NewGuid(),
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                FirstName = request.FirstName,
                FirstTrainingDate = request.FirstTrainingDate,
                Level = request.Level,
                Project = request.Project,
                Sex = request.Sex,
                Status = request.Status,
                Trainings = request.Trainings
            };
    }
}
