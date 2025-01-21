using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Options;
using Sd.Crm.Backend.Controllers.Requests.Squad;
using Sd.Crm.Backend.Controllers.Responses;
using Initializer = Google.Apis.Services.BaseClientService.Initializer;

namespace Sd.Crm.Backend.Services.Google
{
    public class GoogleAccessService : IGoogleAccessService
    {
        private readonly GoogleOptions _options;
        private readonly MappingService _mappingService;

        public GoogleAccessService(IOptions<GoogleOptions> options) 
        {
            _options = options.Value;
            _mappingService = new MappingService();
        }
        public Task<SquadResponse> GetSquad(SpreadsheetRequest spreadSheetRequest, CancellationToken ct)
        {
            var service = GetSheetsService();

            var spreadSheetsId = spreadSheetRequest.GoogleId;
            var sheet = spreadSheetRequest.SheetName ?? "2024-2025";
            int tableLength = 50;
            var separator = string.IsNullOrWhiteSpace(sheet) ? string.Empty : "!";
            var range = $"{sheet}{separator}A1:BW{tableLength}";

            var request = service.Spreadsheets.Values.Get(spreadSheetsId, range);

            var response = request.Execute();
            var values = response.Values;

            return Task.FromResult(values.ToSquad(_mappingService.GetTableMapping()));
        }

        public Task<IEnumerable<Model.LeadModels.Lead>> GetLeads(CancellationToken ct)
        {
            var service = GetSheetsService();

            var separator = string.IsNullOrWhiteSpace(_options.LeadSheetName) ? string.Empty : "!";
            var range = $"{_options.LeadSheetName}{separator}A1:BW{_options.LeadDeapth}";

            var request = service.Spreadsheets.Values.Get(_options.LeadId, range);

            var response = request.Execute();
            var values = response.Values;

            return Task.FromResult(values.ToLeadCollection(_mappingService.GetLeadMapping()));
        }

        private SheetsService GetSheetsService()
        {
            GoogleCredential credentials;

            using (var stream = new FileStream(_options.CradentialPath, FileMode.Open, FileAccess.Read))
            {
                credentials = GoogleCredential.FromStream(stream);
            }

            var service = new SheetsService(new Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "SdSheets"
            });
            
            return service;
        }
    }
}
