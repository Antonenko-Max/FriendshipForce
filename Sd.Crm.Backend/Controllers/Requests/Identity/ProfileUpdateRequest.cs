namespace Sd.Crm.Backend.Controllers.Requests.Identity
{
    public class ProfileUpdateRequest
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
    }
}
