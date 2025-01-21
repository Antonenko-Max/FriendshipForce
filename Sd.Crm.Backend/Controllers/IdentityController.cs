using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Sd.Crm.Backend.Authorization;
using Sd.Crm.Backend.Controllers.Requests.Identity;
using Sd.Crm.Backend.Exceptions;
using Sd.Crm.Backend.Services.User;
using System.Security.Claims;

namespace Sd.Crm.Backend.Controllers
{
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _userService;

        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = await _userService.Register(request);

                var claims = new List<Claim> {
                    new Claim(AuthorizationConstants.UserName, user.Email),
                };
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                await HttpContext.SignInAsync(principal);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] RegisterRequest request)
        {
            try
            {
                var user = await _userService.Login(request);

                var claims = new List<Claim> {
                    new Claim(AuthorizationConstants.UserName, user.Email),
                };
                claims.AddRange(user.Claims?.Select(c => new Claim(c.Name, c.Value)));
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                await HttpContext.SignInAsync(principal);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Route("signout")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return SignOut();
        }

        [Route("profile/{id}")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromRoute] Guid id, [FromBody] ProfileUpdateRequest request)
        {
            try
            {
                var result = await _userService.Update(id, request);
                return Ok(result);
            }
            catch (NotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message);
            }
        }

        [Route("mentor/{userId}")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeMentor([FromRoute] Guid userId)
        {
            var mentor = await _userService.MakeMentor(userId);
            return Ok(mentor);
        }

        [Route("admin/{userId}")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeAdmin([FromRoute] Guid userId)
        {
            var mentor = await _userService.MakeAdmin(userId);
            return Ok(mentor);
        }
    }
}
