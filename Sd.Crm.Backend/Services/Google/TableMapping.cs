namespace Sd.Crm.Backend.Services.Google
{
    public class TableMapping
    {
        public int DiscipleListStartsFrom { get; set; }
        public int TrainingsStartsFrom { get; set; }
        public int StartingYear { get; set; }
        public Dictionary<string, int> ColumnMapping { get; set; }
    }
}
