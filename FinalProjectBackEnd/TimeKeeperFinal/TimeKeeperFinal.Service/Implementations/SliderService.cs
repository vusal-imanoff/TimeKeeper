using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.SliderDTO;
using TimeKeeperFinal.Service.Extentions;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class SliderService : ISliderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SliderService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }
        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }


            Slider slider = await _unitOfWork.SliderRepository.GetAsync(s => s.Id == id && !s.IsDeleted);
            if (slider == null)
            {
                throw new NotFoundException("slider not found");
            }

            slider.IsDeleted = true;
            slider.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<SliderListDTO>> GetAllAsync()
        {
            List<SliderListDTO> sliderListDTOs = _mapper.Map<List<SliderListDTO>>(await _unitOfWork.SliderRepository.GetAllAsync());
            return sliderListDTOs;
        }

        public async Task<List<SliderListDTO>> GetAllForUsersAsync()
        {
            List<SliderListDTO> sliderListDTOs = _mapper.Map<List<SliderListDTO>>(await _unitOfWork.SliderRepository.GetAllForUsersAsync(s=>!s.IsDeleted));
            return sliderListDTOs;
        }

        public async Task<SliderGetDTO> GetByIdAsync(int? id)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }
            SliderGetDTO sliderGetDTO = _mapper.Map<SliderGetDTO>(await _unitOfWork.SliderRepository.GetAsync(s => s.Id == id));
            return sliderGetDTO;
        }

        public async Task PostAsync(SliderPostDTO sliderPostDTO)
        {
            
            Slider slider = _mapper.Map<Slider>(sliderPostDTO);
            if (sliderPostDTO.File != null)
            {
                slider.Image = await sliderPostDTO.File.CreateFileAsync(_env, "sliders");
            }
            await _unitOfWork.SliderRepository.AddAsync(slider);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, SliderPutDTO sliderPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            if (sliderPutDTO.Id!=id)
            {
                throw new BadRequestException("id is not match");
            }

            Slider slider = await _unitOfWork.SliderRepository.GetAsync(s => s.Id == id && !s.IsDeleted);
            if (slider==null)
            {
                throw new NotFoundException("slider not found");
            }

            if (sliderPutDTO.File != null)
            {
                if (slider.Image != null)
                {
                    string fullpath = Path.Combine(_env.WebRootPath, "sliders", slider.Image);
                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }

                slider.Image = await sliderPutDTO.File.CreateFileAsync(_env, "sliders");

            }

            slider.MainTitle = sliderPutDTO.MainTitle;
            slider.SubTitle = sliderPutDTO.SubTitle;
            slider.Description = sliderPutDTO.Description;
            slider.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }


            Slider slider = await _unitOfWork.SliderRepository.GetAsync(s => s.Id == id && s.IsDeleted);
            if (slider == null)
            {
                throw new NotFoundException("slider not found");
            }

            slider.IsDeleted = false;
            slider.DeletedAt = null;

            await _unitOfWork.CommitAsync();
        }
    }
}
