using System.Text.Json.Serialization;

namespace Sd.Crm.Backend.Model.SquadModels
{
    public class Disciple
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Sex { get; set; }
        public SdProject? Project { get; set; }
        public DiscipleLevel? Level { get; set; }
        public string? FirstTrainingDate { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DiscipleStatusEnum? Status { get; set; }
        public Mother? Mother { get; set; }
        public Father? Father { get; set; }
        [JsonIgnore]
        public Squad Squad { get; set; }
        public List<Training> Trainings { get; set; } = new List<Training>();
    }

}
