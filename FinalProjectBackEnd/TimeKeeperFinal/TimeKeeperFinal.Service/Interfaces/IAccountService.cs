using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.AccountDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterDTO registerDTO);
        Task<string> LoginAsync(LoginDTO loginDTO);
        Task ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);
        Task UpdateAsync(ProfileDTO profileDTO);
        Task UpdatePasswordAsync(UpdatePasswordDTO updatePasswordDTO);
    }
}
