using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Core.Entities
{
    public class ProductItem: BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Nullable<int> ColorId { get; set; }
        public Color Color { get; set; }
        public Nullable<int> SizeId { get; set; }
        public Size Size { get; set; }
        public int Count { get; set; }
    }
}
