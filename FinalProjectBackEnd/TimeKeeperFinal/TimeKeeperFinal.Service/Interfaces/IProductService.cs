using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.ProductDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface IProductService
    {
        Task PostAsync(ProductPostDTO productPostDTO);
        Task<PagenetedListDTO<ProductListDTO>> GetAllPageIndexAsync(int pageIndex);
        Task<List<ProductListDTO>> GetAllForUsersAsync();
        Task<List<ProductListDTO>> GetAllAsync();
        Task<ProductGetDTO> GetByIdAsync(int? id);
        Task PutAsync(int? id, ProductPutDTO productPutDTO);
        Task DeleteAsync(int? id);
        Task RestoreAsync(int? id);
    }
}
