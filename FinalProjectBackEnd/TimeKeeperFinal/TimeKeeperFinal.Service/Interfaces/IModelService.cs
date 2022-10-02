using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.ModelDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface IModelService
    {
        Task PostAsync(ModelPostDTO modelPostDTO);

        Task<List<ModelListDTO>> GetAllAsync();
        Task<List<ModelListDTO>> GetAllForUsersAsync();

        Task<ModelGetDTO> GetByIdAsync(int? id);

        Task PutAsync(int? id, ModelPutDTO modelPutDTO);
        Task DeleteAsync(int? id);
        Task RestoreAsync(int? id);
    }
}
