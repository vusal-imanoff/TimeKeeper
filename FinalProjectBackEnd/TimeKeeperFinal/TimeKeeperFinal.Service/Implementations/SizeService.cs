using AutoMapper;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.SizeDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SizeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }


            Size size = await _unitOfWork.SizeRepository.GetAsync(s => s.Id == id && !s.IsDeleted);
            if (size == null)
            {
                throw new NotFoundException("size not found");
            }

            size.IsDeleted = true;
            size.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<SizeListDTO>> GetAllAsync()
        {
            List<SizeListDTO> sizeListDTOs = _mapper.Map<List<SizeListDTO>>(await _unitOfWork.SizeRepository.GetAllAsync());
            return sizeListDTOs;
        }

        public async Task<List<SizeListDTO>> GetAllForUsersAsync()
        {
            List<SizeListDTO> sizeListDTOs = _mapper.Map<List<SizeListDTO>>(await _unitOfWork.SizeRepository.GetAllForUsersAsync(s => !s.IsDeleted));
            return sizeListDTOs;
        }

        public async Task<SizeGetDTO> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            SizeGetDTO sizeGetDTO = _mapper.Map<SizeGetDTO>(await _unitOfWork.SizeRepository.GetAsync(s => s.Id == id));
            return sizeGetDTO;
        }

        public async Task PostAsync(SizePostDTO sizePostDTO)
        {
            if (await _unitOfWork.SizeRepository.IsExistsAsync(s => s.Name == sizePostDTO.Name))
            {
                throw new AlreadyExistsException($"{sizePostDTO.Name} is exists");
            }

            Size size = _mapper.Map<Size>(sizePostDTO);

            await _unitOfWork.SizeRepository.AddAsync(size);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, SizePutDTO sizePutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            if (sizePutDTO.Id != id)
            {
                throw new BadRequestException("id is not match");
            }

            Size size = await _unitOfWork.SizeRepository.GetAsync(s => s.Id == id && !s.IsDeleted);
            if (size == null)
            {
                throw new NotFoundException("size not found");
            }

            if (await _unitOfWork.SizeRepository.IsExistsAsync(s => s.Id != sizePutDTO.Id && s.Name == sizePutDTO.Name))
            {
                throw new AlreadyExistsException($"{sizePutDTO.Name} is exists");
            }

            size.Name = sizePutDTO.Name;
            size.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Size size = await _unitOfWork.SizeRepository.GetAsync(s => s.Id == id && s.IsDeleted);
            if (size == null)
            {
                throw new NotFoundException("size not found");
            }

            size.IsDeleted = false;
            size.UpdatedAt = null;

            await _unitOfWork.CommitAsync();
        }
    }
}
