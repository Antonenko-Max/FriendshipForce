using System.Text.Json;

namespace Sd.Crm.Backend.Services.Google
{
    public class MappingService
    {
        private readonly TableMapping? tableMapping;
        private readonly Dictionary<string, int>? leadMapping;

        public MappingService()
        {
            using (var file = new FileStream("./Json/defaultTableMapping.json", FileMode.Open, FileAccess.Read))
            {
                tableMapping = JsonSerializer.Deserialize<TableMapping>(file, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            }
            using (var file = new FileStream("./Json/defaultLeadMapping.json", FileMode.Open, FileAccess.Read))
            {
                leadMapping = JsonSerializer.Deserialize<Dictionary<string, int>>(file, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }

        }
        public TableMapping GetTableMapping()
        {
            if (tableMapping == null)
            {
                throw new Exception("No squad mapping found");
            }

            return tableMapping;
        }

        public Dictionary<string, int> GetLeadMapping()
        {
            if (leadMapping == null)
            {
                throw new Exception("No lead mapping found");
            }

            return leadMapping;

        }
    }
}
