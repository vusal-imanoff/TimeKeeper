using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.Extentions;

namespace TimeKeeperFinal.Service.DTOs.ProductDTO
{
    public class ProductPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public IFormFile FirstFile { get; set; }
        public IFormFile SecondFile { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<int> TagIds { get; set; }
        public List<ProductImages> ProductImages { get; set; }
        public bool Availability { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
        public int BrandId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public class ProductPutValidator : AbstractValidator<ProductPutDTO>
        {
            public ProductPutValidator()
            {
                RuleFor(p => p.Name).NotEmpty().MaximumLength(255);
                RuleFor(b => b.Description).NotEmpty().MaximumLength(1000);
                RuleFor(b => b.Price).NotEmpty();
                RuleFor(b => b.DiscountPrice).NotEmpty();
                RuleFor(p => p.Code).NotEmpty().MaximumLength(255);
                RuleFor(p => p.Count).NotEmpty();
                RuleFor(b => b.BrandId).NotEmpty();
                RuleFor(b => b.ModelId).NotEmpty();
                RuleFor(b => b.CategoryId).NotEmpty();
            }
        }
    }
}
