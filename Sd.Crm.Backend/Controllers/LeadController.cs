using Microsoft.AspNetCore.Mvc;
using Sd.Crm.Backend.Authorization;
using Sd.Crm.Backend.Services.Lead;

namespace Sd.Crm.Backend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _leadService;
        public LeadController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        [ActionName("")]
        [HttpGet]
        [MentorAuthorize]
        public async Task<IActionResult> GetSquadsByMentor(CancellationToken ct)
        {
            var result = await _leadService.LoadLeadFromSpreadSheet(ct);
            return Ok(result);
        }
    }
}
