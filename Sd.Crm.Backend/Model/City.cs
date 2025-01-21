using System.Text.Json.Serialization;

namespace Sd.Crm.Backend.Model
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public Region Region { get; set; }
    }
}
