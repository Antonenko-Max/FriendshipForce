using Sd.Crm.Backend.Controllers.Requests.Squad;
using Sd.Crm.Backend.Controllers.Responses;

namespace Sd.Crm.Backend.Services.Squad
{
    public interface ISquadService
    {
        Task<SquadResponse> GetSquad(Guid id, CancellationToken ct);
        Task<Model.SquadModels.Squad> PostSquad(SquadRequest squad, CancellationToken ct);
        Task<SquadResponse> LoadSquadFromSpreadSheet(SpreadsheetRequest request, CancellationToken ct);
        Task<IEnumerable<Model.SquadModels.Squad>> GetSquadsByMentor(Guid id, CancellationToken ct);
        Task<IEnumerable<Model.SquadModels.Squad>> GetSquadsByCity(string city, CancellationToken ct);
    }
}
