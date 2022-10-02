using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.ProductItemDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface IProductItemService
    {
        Task PostAsync(ProductItemPostDTO productItemPostDTO);

        Task<List<ProductItemListDTO>> GetAllAsync();
        Task<List<ProductItemListDTO>> GetAllForUsersAsync();

        Task<ProductItemGetDTO> GetByIdAsync(int? id);

        Task PutAsync(int? id, ProductItemPutDTO productItemPutDTO);

        Task DeleteAsync(int? id);

        Task RestoreAsync(int? id);
    }
}
