using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.UserDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = ("SuperAdmin"))]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            await _userService.RegisterAsync(userRegisterDTO);
            return StatusCode(201);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserUpdateDTO userUpdateDTO)
        {
            await _userService.UpdateAsync(userUpdateDTO);
            return StatusCode(201);
        }

        [HttpPost("reset/{id}")]
        public async Task<IActionResult> ResetPassword(string id, ResetPasswordDTO resetPasswordDTO)
        {
            await _userService.ResetPasswordAsync(id, resetPasswordDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeActive(string id)
        {
            await _userService.DeActiveAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Active(string id)
        {
            await _userService.ActiveAsync(id);
            return NoContent();
        }
    }
}
