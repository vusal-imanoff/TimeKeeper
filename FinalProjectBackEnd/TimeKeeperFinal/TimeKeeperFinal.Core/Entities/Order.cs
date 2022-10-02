using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Core.Entities
{
    public class Order :BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int AddressInformationId { get; set; }
        public AddressInformation AddressInformation { get; set; }
        public int Count { get; set; }
       
        public double TotalPrice { get; set; }
    }
}
