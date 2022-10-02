﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.CategoryDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface ICategoryService
    {
        Task PostAsync(CategoryPostDTO categoryPostDTO);

        Task<List<CategoryListDTO>> GetAllAsync();
        Task<List<CategoryListDTO>> GetAllForUsersAsync();

        Task<CategoryGetDTO> GetByIdAsync(int? id);

        Task PutAsync(int? id, CategoryPutDTO categoryPutDTO);

        Task DeleteAsync(int? id);

        Task RestoreAsync(int? id);
    }
}
