using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.AddressInformationDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface IAddressInformationService
    {
        Task PostAsync(AddressInformationPostDTO addressInformationPostDTO);

        Task<List<AddressInformationListDTO>> GetAllAsync(string username);
        Task<List<AddressInformationListDTO>> GetAllForUsersAsync(string username);

        Task<AddressInformationGetDTO> GetByIdAsync(int? id, string username);

        Task PutAsync(int? id, AddressInformationPutDTO addressInformationPutDTO);

        Task DeleteAsync(int? id);

        Task RestoreAsync(int? id);
    }
}
