using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sd.Crm.Backend.Controllers
{
    public class TestController : ControllerBase
    {
        [HttpGet("test")]
        public async Task<ActionResult> Test()
        {
            GoogleCredential credentials;

            using (var stream = new FileStream("./Json/genial-runway-444023-t0-f1df53f7987e.json", FileMode.Open, FileAccess.Read))
            {
                credentials = GoogleCredential.FromStream(stream);
            }
            //var result = await client.GetAsync("/gviz/tq?tqx=out:csv&sheet={23010640}");

            var service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "SdSheets"
            });


            var spreadSheetsId = "1o-RvZKwQdyiM_K8YxfVKw3ZMmBZPNVuWId6fwy4waPY";
            var sheet = "2024-2025";
            var range = $"{sheet}!A1:F10";

            var request = service.Spreadsheets.Values.Get(spreadSheetsId, range);

            var response = request.Execute();
            var values = response.Values;

            return Ok(values);
        }

        [HttpGet("about")]
        [Authorize(Policy = "read_application_requests")]
        public async Task<IActionResult> About()
        {
            return Ok("About");
        }
    }
}
