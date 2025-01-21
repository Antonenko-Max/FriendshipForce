namespace Sd.Crm.Backend.Model.UserModels
{
    public class UserClaim
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public User User { get; set; }

        public UserClaim() { }

        public UserClaim(Guid id, string name, string value, User user)
        {
            Id = id;
            Name = name;
            Value = value;
            User = user;
        }
    }
}
