namespace Sd.Crm.Backend.Model.SquadModels
{
    public class DiscipleLevel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DiscipleLevel(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
