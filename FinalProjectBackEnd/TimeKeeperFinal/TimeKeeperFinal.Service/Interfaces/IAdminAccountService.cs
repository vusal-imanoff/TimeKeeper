using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.AccountDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface IAdminAccountService
    {
        Task<string> LoginAsync(LoginDTO loginDTO);
        Task LogoutAsync();
    }
}
