using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.AddressInformationDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class AddressInformationService : IAddressInformationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AddressInformationService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            AddressInformation addressInformation = await _unitOfWork.AddressInformationRepository.GetAsync(a => a.Id == id && !a.IsDeleted);

            if (addressInformation == null)
            {
                throw new NotFoundException("address information not found");
            }
            addressInformation.IsDeleted = true;
            addressInformation.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<AddressInformationListDTO>> GetAllAsync(string username)
        {
            AppUser appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) throw new NotFoundException("User not found");

            List<AddressInformationListDTO> addressInformationListDTOs = _mapper.Map<List<AddressInformationListDTO>>(await _unitOfWork.AddressInformationRepository.GetAllAsync(/*a => !a.IsDeleted && a.AppUserId==appUser.Id*/));
           
            return addressInformationListDTOs;
        }

        public async Task<List<AddressInformationListDTO>> GetAllForUsersAsync(string username)
        {
            AppUser appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) throw new NotFoundException("User not found");

            List<AddressInformationListDTO> addressInformationListDTOs = _mapper.Map<List<AddressInformationListDTO>>(await _unitOfWork.AddressInformationRepository.GetAllForUsersAsync(a => !a.IsDeleted && a.AppUserId == appUser.Id));

            return addressInformationListDTOs;
        }

        public async Task<AddressInformationGetDTO> GetByIdAsync(int? id,string username)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }
            AppUser appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) throw new NotFoundException("User not found");


            AddressInformationGetDTO addressInformationGetDTO = _mapper.Map<AddressInformationGetDTO>(await _unitOfWork.AddressInformationRepository.GetAsync(a => a.Id == id && a.AppUserId==appUser.Id));
            return addressInformationGetDTO;
        }

        public async Task PostAsync(AddressInformationPostDTO addressInformationPostDTO)
        {
            if (await _unitOfWork.AddressInformationRepository.IsExistsAsync(a=>a.Address==addressInformationPostDTO.Address))
            {
                throw new AlreadyExistsException($"{addressInformationPostDTO.Address} is exists");
            }

            AddressInformation addressInformation = _mapper.Map<AddressInformation>(addressInformationPostDTO);
            await _unitOfWork.AddressInformationRepository.AddAsync(addressInformation);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, AddressInformationPutDTO addressInformationPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            if (id!=addressInformationPutDTO.Id)
            {
                throw new BadRequestException("id is not match");
            }

            AddressInformation addressInformation = await _unitOfWork.AddressInformationRepository.GetAsync(a => a.Id == id && !a.IsDeleted);

            if (addressInformation==null)
            {
                throw new NotFoundException("address information not found");
            }

            if (await _unitOfWork.AddressInformationRepository.IsExistsAsync(a=>a.Id!=addressInformationPutDTO.Id && a.Name==addressInformationPutDTO.Name))
            {
                throw new AlreadyExistsException($"{addressInformationPutDTO.Address} is exists");
            }

            addressInformation.AppUserId = addressInformationPutDTO.AppUserId;
            addressInformation.Name = addressInformationPutDTO.Name;
            addressInformation.SurName = addressInformationPutDTO.SurName;
            addressInformation.Email = addressInformationPutDTO.Email;
            addressInformation.Phone = addressInformationPutDTO.Phone;
            addressInformation.Address = addressInformationPutDTO.Address;
            addressInformation.Country = addressInformationPutDTO.Country;
            addressInformation.City = addressInformationPutDTO.City;
            addressInformation.ZipCode = addressInformationPutDTO.ZipCode;
            addressInformation.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            AddressInformation addressInformation = await _unitOfWork.AddressInformationRepository.GetAsync(a => a.Id == id && a.IsDeleted);

            if (addressInformation == null)
            {
                throw new NotFoundException("address information not found");
            }
            addressInformation.IsDeleted = false;
            addressInformation.DeletedAt = null;

            await _unitOfWork.CommitAsync();
        }
    }
}

