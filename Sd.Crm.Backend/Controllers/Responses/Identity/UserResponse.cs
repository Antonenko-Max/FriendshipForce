namespace Sd.Crm.Backend.Controllers.Responses.Identity
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string Email { get; set; }
        public List<ClaimResponse>? Claims { get; set; }
    }
}
