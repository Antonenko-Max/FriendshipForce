using System.Collections.Specialized;

namespace Sd.Crm.Backend.Controllers.Responses.Identity
{
    public class ClaimResponse
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ClaimResponse(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
