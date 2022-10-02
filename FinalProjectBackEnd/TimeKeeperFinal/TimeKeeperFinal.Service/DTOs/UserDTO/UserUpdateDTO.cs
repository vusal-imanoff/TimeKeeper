using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.UserDTO
{
    public class UserUpdateDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeActive { get; set; }
        public string PhoneNumber { get; set; }
    }
}
