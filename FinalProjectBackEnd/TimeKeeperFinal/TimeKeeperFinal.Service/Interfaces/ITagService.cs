using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.TagDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface ITagService
    {
        Task PostAsync(TagPostDTO tagPostDTO);

        Task<List<TagListDTO>> GetAllAsync();
        Task<List<TagListDTO>> GetAllForUsersAsync();

        Task<TagGetDTO> GetByIdAsync(int? id);

        Task PutAsync(int? id, TagPutDTO tagPutDTO);

        Task DeleteAsync(int? id);

        Task RestoreAsync(int? id);
    }
}
