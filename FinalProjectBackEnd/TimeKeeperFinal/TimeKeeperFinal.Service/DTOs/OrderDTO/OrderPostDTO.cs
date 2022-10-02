using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.OrderDTO
{
    public class OrderPostDTO
    {
        public string AppUserId { get; set; }
        public int ProductId { get; set; }
        public int AddressInformationId { get; set; }
        public int Count { get; set; }
        public double TotalPrice { get; set; }
    }

    public class OrderPostValidator :AbstractValidator<OrderPostDTO>
    {
        public OrderPostValidator()
        {
            RuleFor(o => o.ProductId).NotNull();
            RuleFor(o => o.AddressInformationId).NotNull();
            RuleFor(o => o.Count).NotNull();
            RuleFor(o => o.TotalPrice).NotNull();
        }
    }
}
