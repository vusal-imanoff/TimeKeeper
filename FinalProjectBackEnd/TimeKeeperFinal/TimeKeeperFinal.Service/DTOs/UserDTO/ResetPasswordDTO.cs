using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.UserDTO
{
    public class ResetPasswordDTO
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
