using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.ProductItemDTO
{
    public class ProductItemListDTO
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }
        public string ColorCode { get; set; }
        public string MainImage { get; set; }
        public string SecondImage { get; set; }
        public bool IsDeleted { get; set; }
    }
}
