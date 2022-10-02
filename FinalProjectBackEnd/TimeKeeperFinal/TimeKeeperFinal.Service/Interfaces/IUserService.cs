using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.UserDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task UpdateAsync (UserUpdateDTO userUpdateDTO);
        Task DeActiveAsync(string id);
        Task ActiveAsync(string id);
        Task ResetPasswordAsync(string id, ResetPasswordDTO resetPasswordDTO);
        Task<List<UserListDTO>> GetAll();
    }
}
