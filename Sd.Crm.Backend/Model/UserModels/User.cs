namespace Sd.Crm.Backend.Model.UserModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public List<UserClaim>? Claims { get; set; }
    }
}
