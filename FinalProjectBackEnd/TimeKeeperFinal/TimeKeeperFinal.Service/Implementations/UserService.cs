using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.UserDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task ActiveAsync(string id)
        {
            if (id == null) throw new BadRequestException("id is required");

            AppUser appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null) throw new NotFoundException("user not found");
            appUser.IsDeActive = false;
            await _unitOfWork.CommitAsync();
        }

        public async Task DeActiveAsync(string id)
        {
            if (id == null) throw new BadRequestException("id is required");

            AppUser appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null) throw new NotFoundException("user not found");
            appUser.IsDeActive = true;
            await _unitOfWork.CommitAsync();
            
        }
        public async Task<List<UserListDTO>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            List<UserListDTO> userListDTOs = new List<UserListDTO>();
            foreach (var user in users)
            {
                userListDTOs.Add(_mapper.Map<UserListDTO>(user));
            }
            return userListDTOs;
        }

        public async Task RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            AppUser appUser = _mapper.Map<AppUser>(userRegisterDTO);

            IdentityResult identityResult = await _userManager.CreateAsync(appUser, userRegisterDTO.Password);

            if (userRegisterDTO.IsAdmin == true)
            {
                await _userManager.AddToRoleAsync(appUser, "Admin");
                appUser.IsAdmin = true;
            }
            else
            {
                await _userManager.AddToRoleAsync(appUser, "Member");

            }

            if (!identityResult.Succeeded)
            {
                throw new BadRequestException(identityResult.Errors.ToString());
            }
        }

        public async Task ResetPasswordAsync(string id, ResetPasswordDTO resetPasswordDTO)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }

            AppUser appUser = await _userManager.FindByIdAsync(id);

            if (appUser==null)
            {
                throw new NotFoundException("user not found");
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            await _userManager.ResetPasswordAsync(appUser, token, resetPasswordDTO.Password);
        }

        public async Task UpdateAsync(UserUpdateDTO userUpdateDTO)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userUpdateDTO.Id);
            if (appUser==null)
            {
                throw new NotFoundException("appuser not found");
            }

            appUser.Name = userUpdateDTO.Name;
            appUser.Email = userUpdateDTO.Email;
            appUser.SurName = userUpdateDTO.SurName;
            appUser.PhoneNumber = userUpdateDTO.PhoneNumber;
            appUser.UserName = userUpdateDTO.UserName;

            IdentityResult identity = await _userManager.UpdateAsync(appUser);

            if (!identity.Succeeded)
            {
                foreach (var item in identity.Errors)
                {
                    throw new BadRequestException(item.Description.ToString());
                }
            }
        }
    }
}
