using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Core.Entities
{
    public class ProductImages : BaseEntity
    {
        public string Image { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
