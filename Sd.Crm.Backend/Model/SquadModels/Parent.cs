namespace Sd.Crm.Backend.Model.SquadModels
{
    public abstract class Parent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Comment { get; set; }
    }
}
