using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string MainImage { get; set; }
        public string SecondImage { get; set; }
        public bool Availability { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
        public int BasketCount { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsMost { get; set; }
        public bool IsBestSeller { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Model Model { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductImages> ProductImages { get; set; }
        public List<ProductTag> ProductTags { get; set; }
    }
}
