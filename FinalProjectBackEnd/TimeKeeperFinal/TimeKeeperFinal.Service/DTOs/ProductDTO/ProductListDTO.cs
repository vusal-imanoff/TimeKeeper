using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;

namespace TimeKeeperFinal.Service.DTOs.ProductDTO
{
    public class ProductListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string MainImage { get; set; }
        public string SecondImage { get; set; }
        public bool Availability { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
