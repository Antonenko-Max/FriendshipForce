using Microsoft.AspNetCore.Mvc;
using Sd.Crm.Backend.Controllers.Requests.Internal;
using Sd.Crm.Backend.Services.Internal;

namespace Sd.Crm.Backend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class InternalController : ControllerBase
    {
        private readonly IInternalService _internalService;

        public InternalController(IInternalService internalService)
        {
            _internalService = internalService;
        }

        [ActionName("project")]
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] SdProjectCreateRequest project, CancellationToken ct)
        {
            var result = await _internalService.CreateSdProject(project, ct);
            return Ok(result);
        }

        [ActionName("project/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromRoute] Guid id, [FromBody] SdProjectCreateRequest project, CancellationToken ct)
        {
            var result = await _internalService.UpdateSdProject(id, project, ct);
            return Ok(result);
        }

        [ActionName("project/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetProject([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _internalService.GetSdProject(id, ct);
            return Ok(result);
        }

        [ActionName("project/name/{name}")]
        [HttpGet]
        public async Task<IActionResult> GetProjectByName([FromRoute] string name, CancellationToken ct)
        {
            var result = await _internalService.GetSdProjectByName(name, ct);
            return Ok(result);
        }

        [ActionName("project")]
        [HttpGet]
        public async Task<IActionResult> GetProjects(CancellationToken ct)
        {
            var result = await _internalService.GetSdProjects(ct);
            return Ok(result);
        }

        [ActionName("project/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProject([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _internalService.DeleteSdProject(id, ct);
            return Ok(result);
        }

        [ActionName("level")]
        [HttpPost]
        public async Task<IActionResult> Createlevel([FromBody] DiscipleLevelRequest level, CancellationToken ct)
        {
            var result = await _internalService.CreateLevel(level, ct);
            return Ok(result);
        }

        [ActionName("level/{id}")]
        [HttpPut]
        public async Task<IActionResult> Updatelevel([FromRoute] Guid id, [FromBody] DiscipleLevelRequest level, CancellationToken ct)
        {
            var result = await _internalService.UpdateLevel(id, level, ct);
            return Ok(result);
        }

        [ActionName("level/{id}")]
        [HttpGet]
        public async Task<IActionResult> Getlevel([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _internalService.GetLevel(id, ct);
            return Ok(result);
        }

        [ActionName("level/name/{name}")]
        [HttpGet]
        public async Task<IActionResult> GetlevelByName([FromRoute] string name, CancellationToken ct)
        {
            var result = await _internalService.GetLevelByName(name, ct);
            return Ok(result);
        }

        [ActionName("level")]
        [HttpGet]
        public async Task<IActionResult> Getlevels(CancellationToken ct)
        {
            var result = await _internalService.GetLevels(ct);
            return Ok(result);
        }

        [ActionName("level/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Deletelevel([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _internalService.DeleteLevel(id, ct);
            return Ok(result);
        }
    }
}
