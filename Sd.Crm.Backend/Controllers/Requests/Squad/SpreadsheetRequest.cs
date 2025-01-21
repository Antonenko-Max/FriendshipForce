namespace Sd.Crm.Backend.Controllers.Requests.Squad
{
    public class SpreadsheetRequest
    {
        public string GoogleId { get; set; }
        public Guid MentorId { get; set; }
        public string? Name { get; set; }
        public string City { get; set; }
        public string? Location { get; set; }
        public string? SheetName { get; set; }
    }
}
