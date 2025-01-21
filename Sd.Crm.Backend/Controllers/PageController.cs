using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sd.Crm.Backend.Services.User;

namespace Sd.Crm.Backend.Controllers
{
    public class PageController : ControllerBase
    {
        private readonly IUserService _userService;

        public PageController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("admin/users")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();
            return Ok(result);
        }

        [Route("admin/mentors")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMentors()
        {
            var result = await _userService.GetMentors();
            return Ok(result);
        }

    }
}
