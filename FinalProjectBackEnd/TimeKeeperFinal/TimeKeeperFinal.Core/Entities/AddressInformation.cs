using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Core.Entities
{
    public class AddressInformation : BaseEntity
    {
        public string AppUserId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public AppUser AppUser { get; set; }
    }
}
