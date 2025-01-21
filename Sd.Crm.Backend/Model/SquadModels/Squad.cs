using Sd.Crm.Backend.Model.UserModels;

namespace Sd.Crm.Backend.Model.SquadModels
{
    public class Squad
    {
        public Guid? Id { get; set; }
        public User Mentor { get; set; }
        public string? Location { get; set; }
        public string City { get; set; }
        public string? Name { get; set; }
        public List<Disciple> Disciples { get; set; } = new List<Disciple>();
    }
}
