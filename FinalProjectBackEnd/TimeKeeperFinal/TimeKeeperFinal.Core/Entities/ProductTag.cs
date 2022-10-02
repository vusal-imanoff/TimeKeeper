using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Core.Entities
{
    public class ProductTag : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Nullable<int> TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
