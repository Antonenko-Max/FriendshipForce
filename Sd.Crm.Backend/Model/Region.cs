namespace Sd.Crm.Backend.Model
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
}
