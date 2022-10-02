using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.ProductItemDTO
{
    public class ProductItemPutDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int Count { get; set; }
    }
    public class ProductItemPutValidator : AbstractValidator<ProductItemPutDTO>
    {
        public ProductItemPutValidator()
        {
            RuleFor(x => x.ProductId).NotNull();
            RuleFor(x => x.ColorId).NotNull();
            RuleFor(x => x.SizeId).NotNull();
            RuleFor(x => x.Count).NotNull();
        }
    }
}
