using AutoMapper;
using RentalCarFinalProject.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.CategoryDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class CategoryService :ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("Id Is Required");
            }

            Category category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id && !c.IsDeleted);

            if (category == null)
            {
                throw new NotFoundException("category Not Found");
            }

            category.IsDeleted = true;
            category.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();

        }


        public async Task<List<CategoryListDTO>> GetAllAsync()
        {
            List<CategoryListDTO> categoryListDTOs = _mapper.Map<List<CategoryListDTO>>(await _unitOfWork.CategoryRepository.GetAllAsync(/*c => !c.IsDeleted*/));
            return categoryListDTOs;
        }

        public async Task<List<CategoryListDTO>> GetAllForUsersAsync()
        {
            List<CategoryListDTO> categoryListDTOs = _mapper.Map<List<CategoryListDTO>>(await _unitOfWork.CategoryRepository.GetAllForUsersAsync(c => !c.IsDeleted));
            return categoryListDTOs;
        }

        public async Task<CategoryGetDTO> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new BadImageFormatException("Id is Required");
            }

            CategoryGetDTO categoryGetDTO = _mapper.Map<CategoryGetDTO>(await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id));

            return categoryGetDTO;
        }

        public async Task PostAsync(CategoryPostDTO categoryPostDTO)
        {
            if (await _unitOfWork.CategoryRepository.IsExistsAsync(c => c.Name == categoryPostDTO.Name))
            {
                throw new AlreadyExistsException($"{categoryPostDTO.Name} Category AlreadyExists");
            }

            Category category = _mapper.Map<Category>(categoryPostDTO);

            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task PutAsync(int? id, CategoryPutDTO categoryPutDTO)
        {
            if (id == null)
            {
                throw new BadRequestException("Id is Required");
            }

            if (categoryPutDTO.Id != id)
            {
                throw new BadRequestException("Id is not matched");
            }

            Category category = await _unitOfWork.CategoryRepository.GetAsync(c => !c.IsDeleted && c.Id == id);
            if (category == null)
            {
                throw new NotFoundException($"{category.Name} Not Found");
            }
            if (await _unitOfWork.CategoryRepository.IsExistsAsync(c => c.Id != categoryPutDTO.Id && c.Name == categoryPutDTO.Name))
            {
                throw new AlreadyExistsException($"{categoryPutDTO.Name} is Exists");
            }

            category.Name = categoryPutDTO.Name;
            category.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _unitOfWork.CommitAsync();
        }

        public async Task RestoreAsync(int? id)
        {
            if (id == null)
            {
                throw new BadRequestException("Id Is Required");
            }

            Category category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id && c.IsDeleted);

            if (category == null)
            {
                throw new NotFoundException("category Not Found");
            }

            category.IsDeleted = false;
            category.DeletedAt = null;

            await _unitOfWork.CommitAsync();
        }
    }
}
