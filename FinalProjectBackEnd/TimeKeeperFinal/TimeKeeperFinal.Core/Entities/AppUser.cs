using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Core.Entities
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public bool IsDeActive { get; set; }
        public bool IsAdmin { get; set; }
    }
}
