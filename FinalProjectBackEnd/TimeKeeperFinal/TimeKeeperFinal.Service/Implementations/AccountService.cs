using AutoMapper;
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
    public class AccountService : IAccountService
    {
        
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IJwtManager _jwtManager;

        public AccountService(IMapper mapper, UserManager<AppUser> userManager, IJwtManager jwtManager, IEmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtManager = jwtManager;
            _emailService = emailService;
        }
        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(appUser.EmailConfirmed)
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
            else
            {
                throw new BadRequestException($"{appUser} not confirmed");
            }
        }

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {

            AppUser user = await _userManager.FindByEmailAsync(registerDTO.Email);

            if (user != null) throw new AlreadyExistsException($"{user} is exists");

            AppUser appUser = _mapper.Map<AppUser>(registerDTO);

            IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerDTO.Password);

            if (!identityResult.Succeeded)
            {
                throw new BadRequestException(identityResult.Errors.ToString());
            }

            await _userManager.AddToRoleAsync(appUser, "Member");
                

        }

        public async Task UpdatePasswordAsync(UpdatePasswordDTO updatePasswordDTO)
        {
            AppUser appUser = await _userManager.FindByIdAsync(updatePasswordDTO.Id);

            if (updatePasswordDTO.CurrentPassword != null)
            {
                if (updatePasswordDTO.NewPassword == null)
                {
                    throw new BadRequestException("Password is required");
                }

                if (!await _userManager.CheckPasswordAsync(appUser, updatePasswordDTO.CurrentPassword))
                {
                    throw new BadRequestException("current Password is incorrect");
                }

               IdentityResult identity = await _userManager.ChangePasswordAsync(appUser, updatePasswordDTO.CurrentPassword, updatePasswordDTO.NewPassword);

                if (!identity.Succeeded)
                {
                    foreach (var item in identity.Errors)
                    {
                        throw new BadRequestException(item.Description.ToString());
                    }
                }

            }
        }

        public async Task UpdateAsync(ProfileDTO profileDTO)
        {
            AppUser appUser = await _userManager.FindByIdAsync(profileDTO.Id);

            appUser.Name = profileDTO.Name;
            appUser.SurName = profileDTO.SurName;
            appUser.UserName = profileDTO.Username;
            appUser.Email = profileDTO.Email;
            appUser.PhoneNumber = profileDTO.PhoneNumber;

            IdentityResult identity = await _userManager.UpdateAsync(appUser);

            if (!identity.Succeeded)
            {
                foreach (var item in identity.Errors)
                {
                    throw new BadRequestException(item.Description.ToString());
                }
            }
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO)
        {
            AppUser user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);

            if (user is null) throw new NotFoundException("user not found");

            string forgotpasswordtoken = await _userManager.GeneratePasswordResetTokenAsync(user);
            //string url = Url.Action("http://localhost:3000/", "forgotpassword", new { email = user.Email, Id = user.Id, token = forgotpasswordtoken, }, Request.Scheme);
            string url2 = "http://localhost:49293/forgotpassword/" + user.Email + "/token=" + forgotpasswordtoken;
            await _emailService.ForgotPassword(user, url2, forgotPasswordDTO);
        }

    }
}
