using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.AccountDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountService _appUserService;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountsController(RoleManager<IdentityRole> roleManager, IAccountService appUserService, IEmailService emailService, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _appUserService = appUserService;
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            await _appUserService.RegisterAsync(registerDTO);
            AppUser appUser = await _userManager.FindByEmailAsync(registerDTO.Email);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var link = Url.Action("ConfirmEmail", "Accounts", new { userId = appUser.Id, token = code },Request.Scheme,Request.Host.ToString());
            await _emailService.Register(registerDTO, link);
            return Ok();
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, token);
            return Ok();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {

            return Ok(await _appUserService.LoginAsync(loginDTO));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(ProfileDTO profileDTO)
        {
            await _appUserService.UpdateAsync(profileDTO);
            return NoContent();
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(UpdatePasswordDTO updatePasswordDTO)
        {
            await _appUserService.UpdatePasswordAsync(updatePasswordDTO);
            return NoContent();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            await _appUserService.ForgotPasswordAsync(forgotPasswordDTO);
            return Ok();
        }

        #region CreateRole

        //[HttpGet("createrole")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
        //    return Ok();
        //}
        #endregion
    }
}
