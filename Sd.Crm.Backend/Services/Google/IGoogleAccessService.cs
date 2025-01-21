using Sd.Crm.Backend.Controllers.Requests.Squad;
using Sd.Crm.Backend.Controllers.Responses;

namespace Sd.Crm.Backend.Services.Google
{
    public interface IGoogleAccessService
    {
        Task<IEnumerable<Model.LeadModels.Lead>> GetLeads(CancellationToken ct);
        Task<SquadResponse> GetSquad(SpreadsheetRequest request, CancellationToken ct);
    }
}
