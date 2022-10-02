using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.SizeDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface ISizeService
    {
        Task PostAsync(SizePostDTO sizePostDTO);

        Task<List<SizeListDTO>> GetAllAsync();
        Task<List<SizeListDTO>> GetAllForUsersAsync();

        Task<SizeGetDTO> GetByIdAsync(int? id);

        Task PutAsync(int? id, SizePutDTO sizePutDTO);

        Task DeleteAsync(int? id);

        Task RestoreAsync(int? id);
    }
}
