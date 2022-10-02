using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.AccountDTO
{
    public class ProfileDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
