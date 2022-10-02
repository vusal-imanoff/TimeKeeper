using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.AccountDTO;

namespace TimeKeeperFinal.Service.Interfaces
{
    public interface IEmailService
    {
        //Task SendEmailAsync(string mail, string token);
        Task Register(RegisterDTO registerDto, string link);
        Task ForgotPassword(AppUser user, string url, ForgotPasswordDTO forgotPassword);
        //Task ConfirmEmail(string userId, string token);
    }
}
