using AutoMapper;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.ModelDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class ModelService : IModelService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModelService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Model model = await _unitOfWork.ModelRepository.GetAsync(m => !m.IsDeleted && m.Id == id);

            if (model == null)
            {
                throw new NotFoundException("model not found");
            }
            model.IsDeleted = true;
            model.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async  Task<List<ModelListDTO>> GetAllAsync()
        {
            List<ModelListDTO> modelListDTOs = _mapper.Map<List<ModelListDTO>>(await _unitOfWork.ModelRepository.GetAllAsync(/*m => !m.IsDeleted*/));
            return modelListDTOs;
        }

        public async Task<List<ModelListDTO>> GetAllForUsersAsync()
        {
            List<ModelListDTO> modelListDTOs = _mapper.Map<List<ModelListDTO>>(await _unitOfWork.ModelRepository.GetAllForUsersAsync(m => !m.IsDeleted));
            return modelListDTOs;
        }

        public async Task<ModelGetDTO> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            ModelGetDTO modelGetDTO = _mapper.Map<ModelGetDTO>(await _unitOfWork.ModelRepository.GetAsync(m => m.Id == id));
            return modelGetDTO;
        }

        public async Task PostAsync(ModelPostDTO modelPostDTO)
        {
            if (await _unitOfWork.ModelRepository.IsExistsAsync(m => m.Name == modelPostDTO.Name))
            {
                throw new AlreadyExistsException($"{modelPostDTO.Name} is Exists");
            }

            Model model = _mapper.Map<Model>(modelPostDTO);

            await _unitOfWork.ModelRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();

        }

        public async Task PutAsync(int? id, ModelPutDTO modelPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("Id is required");
            }

            if (modelPutDTO.Id != id)
            {
                throw new BadRequestException("Id is not matched");
            }

            Model model = await _unitOfWork.ModelRepository.GetAsync(m => !m.IsDeleted && m.Id == modelPutDTO.Id);
            if (model == null)
            {
                throw new NotFoundException("model not found");
            }
            if (await _unitOfWork.ModelRepository.IsExistsAsync(m => m.Id != modelPutDTO.Id && m.Name == modelPutDTO.Name))
            {
                throw new AlreadyExistsException($"{modelPutDTO.Name} is exists");
            }

            model.Name = modelPutDTO.Name;
            model.BrandId = modelPutDTO.BrandId;
            model.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("id is required");
            }

            Model model = await _unitOfWork.ModelRepository.GetAsync(m => m.IsDeleted && m.Id == id);

            if (model == null)
            {
                throw new NotFoundException("model not found");
            }
            model.IsDeleted = false;
            model.CreatedAt = null;

            await _unitOfWork.CommitAsync();
        }
    }
}
