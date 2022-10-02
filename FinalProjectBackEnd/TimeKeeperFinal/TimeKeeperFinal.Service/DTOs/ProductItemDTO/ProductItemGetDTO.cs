using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.ProductItemDTO
{
    public class ProductItemGetDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string ProductCode { get; set; }
        public bool Availability { get; set; }
        public string Size { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }
        public string ColorCode { get; set; }
        public string MainImage { get; set; }
        public string SecondImage { get; set; }
    }
}
