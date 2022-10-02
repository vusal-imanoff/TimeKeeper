using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core.Entities;

namespace TimeKeeperFinal.Service.JwtManager.Interfaces
{
    public interface IJwtManager
    {
        Task<string> GenerateTokenAsync(AppUser appUser);
        string GetUserNameByToken(string token);
    }
}
