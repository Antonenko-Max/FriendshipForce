using System.Text.Json.Serialization;

namespace Sd.Crm.Backend.Model.SquadModels
{
    public class Training
    {
        public Guid? Id { get; set; }
        public DateTime Date { get; set; }
        public int Month { get; set; }
        public int Number { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PresenceEnum? Presence { get; set; }
        public string? Comment { get; set; }
        [JsonIgnore]
        public Disciple? Disciple { get; set; }
    }
}
