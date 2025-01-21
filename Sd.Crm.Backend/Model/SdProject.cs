namespace Sd.Crm.Backend.Model
{
    public class SdProject
    {
        public SdProject(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
