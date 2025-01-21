using Microsoft.AspNetCore.Mvc;
using Sd.Crm.Backend.Authorization;
using Sd.Crm.Backend.Controllers.Requests.Squad;
using Sd.Crm.Backend.Services.Squad;

namespace Sd.Crm.Backend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SquadController : ControllerBase
    {
        private readonly ISquadService _squadService;
        public SquadController(ISquadService squadService)
        {
            _squadService = squadService;
        }

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [ActionName("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSquad([FromRoute]Guid id, CancellationToken ct)
        {
            var squad = await _squadService.GetSquad(id, ct);
            return Ok(squad);
        }

        [ActionName("")]
        [HttpPost]
        [AdminAuthorize]
        public async Task<IActionResult> PostSquad([FromBody] SquadRequest squad, CancellationToken ct)
        {
            var result = await _squadService.PostSquad(squad, ct);
            return Ok(result);
        }

        [ActionName("spreadsheet")]
        [HttpPost]
        [AdminAuthorize]
        public async Task<IActionResult> AddSquadFromSpreadsheet([FromBody] SpreadsheetRequest request, CancellationToken ct)
        {
            var result = await _squadService.LoadSquadFromSpreadSheet(request, ct);
            return Ok(result);
        }

        [ActionName("mentor/{id}")]
        [HttpGet]
        [MentorAuthorize]
        public async Task<IActionResult> GetSquadsByMentor([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _squadService.GetSquadsByMentor(id, ct);
            return Ok(result);
        }

        [ActionName("city/{city}")]
        [HttpGet]
        [MentorAuthorize]
        public async Task<IActionResult> GetSquadsByCity([FromRoute] string city, CancellationToken ct)
        {
            var result = await _squadService.GetSquadsByCity(city, ct);
            return Ok(result);
        }
    }
}
