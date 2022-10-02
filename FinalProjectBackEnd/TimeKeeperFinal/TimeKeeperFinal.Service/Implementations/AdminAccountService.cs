using Microsoft.AspNetCore.Identity;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.AccountDTO;
using TimeKeeperFinal.Service.Interfaces;
using TimeKeeperFinal.Service.JwtManager.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class AdminAccountService : IAdminAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtManager _jwtManager;
        private readonly SignInManager<AppUser> _signManager;

        public AdminAccountService(UserManager<AppUser> userManager, IJwtManager jwtManager, SignInManager<AppUser> signManager)
        {
            _userManager = userManager;
            _jwtManager = jwtManager;
            _signManager = signManager;
        }
        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (!await _userManager.IsInRoleAsync(appUser, "Member"))
            {
                if (appUser == null)
                {
                    throw new BadRequestException("Email or passwod incorrect");
                }

                if (!await _userManager.CheckPasswordAsync(appUser, loginDTO.Password))
                {
                    throw new BadRequestException("Email or passwod incorrect");
                }

                return await _jwtManager.GenerateTokenAsync(appUser);
            }
            throw new BadRequestException("Your Role is Member. You do not access to enter");
        }

        public async Task LogoutAsync()
        {
            await _signManager.SignOutAsync();

        }
    }
}
