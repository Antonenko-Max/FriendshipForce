namespace Sd.Crm.Backend.Services.Lead
{
    public interface ILeadService
    {
        Task<IEnumerable<Model.LeadModels.Lead>> LoadLeadFromSpreadSheet(CancellationToken ct);
    }
}
