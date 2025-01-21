namespace Sd.Crm.Backend.Model.LeadModels
{
    public class Lead
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string MotherName { get; set; }
        public string Phone { get; set; }
        public Region? Region { get; set; }
        public City City { get; set; }
        public int? ChildAge { get; set; }

        public Lead(Guid id)
        {
            Id = id;
        }
    }
}
