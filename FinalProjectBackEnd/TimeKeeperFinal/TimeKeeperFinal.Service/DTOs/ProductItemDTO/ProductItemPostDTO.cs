using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.ProductItemDTO
{
    public class ProductItemPostDTO
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int Count { get; set; }
    }
    public class ProductItemPostValidator: AbstractValidator<ProductItemPostDTO>
    {
        public ProductItemPostValidator()
        {
            RuleFor(x => x.ProductId).NotNull();
            RuleFor(x => x.ColorId).NotNull();
            RuleFor(x => x.SizeId).NotNull();
            RuleFor(x => x.Count).NotNull();
        }
    }
}
