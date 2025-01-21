using Sd.Crm.Backend.Model.UserModels;

namespace Sd.Crm.Backend.Controllers.Requests.Squad
{
    public class SquadRequest
    {
        public User Mentor { get; set; }
        public string? Location { get; set; }
        public string City { get; set; }
        public string? Name { get; set; }
        public List<DiscipleRequest> Disciples { get; set; } = new List<DiscipleRequest>();

    }
}
