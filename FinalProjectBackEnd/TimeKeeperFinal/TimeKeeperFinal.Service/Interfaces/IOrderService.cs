using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.OrderDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface IOrderService
    {
        Task PostAsync(OrderPostDTO orderPostDTO);

        Task<List<OrderListDTO>> GetAllAsync();
        Task<List<OrderListDTO>> GetAllForUsersAsync(string username);
        Task<OrderGetDTO> GetByIdAsync(int? id,string username);
    }
}
