﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.BrandDTO;
using TimeKeeperFinal.Service.Extentions;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("Id Is Required");
            }
            Brand brand = await _unitOfWork.BrandRepository.GetAsync(b => !b.IsDeleted && b.Id == id);
            if (brand == null)
            {
                throw new NotFoundException("brand not found");
            }
            brand.IsDeleted = true;
            brand.DeletedAt = DateTime.UtcNow.AddHours(4);
            await _unitOfWork.CommitAsync();

        }

        public async Task<List<BrandListDTO>> GetAllAsync()
        {
            List<BrandListDTO> brandListDTO = _mapper.Map<List<BrandListDTO>>(await _unitOfWork.BrandRepository.GetAllAsync(/*b => !b.IsDeleted*/));
            return brandListDTO;
        }

        public async Task<List<BrandListDTO>> GetAllForUsersAsync()
        {
            List<BrandListDTO> brandListDTO = _mapper.Map<List<BrandListDTO>>(await _unitOfWork.BrandRepository.GetAllForUsersAsync(b => !b.IsDeleted));
            return brandListDTO;
        }

        public async Task<BrandGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }
            BrandGetDTO brandGet = _mapper.Map<BrandGetDTO>(await _unitOfWork.BrandRepository.GetAsync(b => b.Id == id));

            return brandGet;
        }

        public async Task PostAsync(BrandPostDTO brandPostDTO)
        {
            if (await _unitOfWork.BrandRepository.IsExistsAsync(b => b.Name == brandPostDTO.Name))
            {
                throw new AlreadyExistsException($"{brandPostDTO.Name} is Exist.");
            }


            Brand brand = _mapper.Map<Brand>(brandPostDTO);
            if (brandPostDTO.File != null)
            {
                brand.Image = await brandPostDTO.File.CreateFileAsync(_env, "brands");
            }

            await _unitOfWork.BrandRepository.AddAsync(brand);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, BrandPutDTO brandPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("Id Is Required");
            }
            if (brandPutDTO.Id != id)
            {
                throw new BadRequestException("Id is not Matched");
            }
            Brand brand = await _unitOfWork.BrandRepository.GetAsync(b => !b.IsDeleted && b.Id == id);
            if (brand == null)
            {
                throw new NotFoundException("brand not found");
            }

            if (await _unitOfWork.BrandRepository.IsExistsAsync(b => b.Name == brandPutDTO.Name && b.Id != brandPutDTO.Id))
            {
                throw new AlreadyExistsException($"{brandPutDTO.Name} Brand Already Exist.");
            }

            if (brandPutDTO.File != null)
            {
                if (brand.Image != null)
                {
                    string fullpath = Path.Combine(_env.WebRootPath, "brands", brand.Image);
                    if (System.IO.File.Exists(fullpath))
                    {
                        System.IO.File.Delete(fullpath);
                    }
                }

                brand.Image = await brandPutDTO.File.CreateFileAsync(_env, "brands");

            }

            brand.Name = brandPutDTO.Name;
            brand.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();

        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("Id Is Required");
            }
            Brand brand = await _unitOfWork.BrandRepository.GetAsync(b => b.IsDeleted && b.Id == id);
            if (brand == null)
            {
                throw new NotFoundException("brand not found");
            }
            brand.IsDeleted = false;
            brand.DeletedAt = null;
            await _unitOfWork.CommitAsync();
        }
    }
}
