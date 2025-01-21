using Sd.Crm.Backend.Controllers.Requests.Internal;
using Sd.Crm.Backend.Model;
using Sd.Crm.Backend.Model.SquadModels;

namespace Sd.Crm.Backend.Services.Internal
{
    public interface IInternalService
    {
        Task<SdProject> CreateSdProject(SdProjectCreateRequest request, CancellationToken ct);
        Task<SdProject> GetSdProject(Guid projectId, CancellationToken ct);
        Task<SdProject> GetSdProjectByName(string name, CancellationToken ct);
        Task<IEnumerable<SdProject>> GetSdProjects(CancellationToken ct);
        Task<SdProject> DeleteSdProject(Guid projectId, CancellationToken ct);
        Task<SdProject> UpdateSdProject(Guid projectId, SdProjectCreateRequest request, CancellationToken ct);
        Task<SdProject> UseProjectFallback(CancellationToken ct);
        Task<DiscipleLevel> GetLevel(Guid Id,  CancellationToken ct);
        Task<DiscipleLevel> GetLevelByName(string name, CancellationToken ct);
        bool TryGetLevelByName(string name, out DiscipleLevel? level);
        Task<IEnumerable<DiscipleLevel>> GetLevels(CancellationToken ct);
        Task<DiscipleLevel> CreateLevel(DiscipleLevelRequest level, CancellationToken ct);
        Task<DiscipleLevel> UpdateLevel(Guid id, DiscipleLevelRequest level, CancellationToken ct);
        Task<DiscipleLevel> DeleteLevel(Guid Id, CancellationToken ct);
    }
}
