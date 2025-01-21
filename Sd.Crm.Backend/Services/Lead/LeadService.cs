using Sd.Crm.Backend.Services.Google;

namespace Sd.Crm.Backend.Services.Lead
{
    public class LeadService : ILeadService
    {
        private readonly IGoogleAccessService _googleAccessService;

        public LeadService(IGoogleAccessService googleAccessService)
        {
            _googleAccessService = googleAccessService;                
        }

        public async Task<IEnumerable<Model.LeadModels.Lead>> LoadLeadFromSpreadSheet(CancellationToken ct)
        {
            var result = await _googleAccessService.GetLeads(ct);
            return result;
        }
    }
}
