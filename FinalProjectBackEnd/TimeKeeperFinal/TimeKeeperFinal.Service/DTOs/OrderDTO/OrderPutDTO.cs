using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.OrderDTO
{
    public class OrderPutDTO
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int ProductId { get; set; }
        public int AddressInformationId { get; set; }
        public string Color { get; set; }
        public double TotalPrice { get; set; }
    }

    public class OrderPutValidator : AbstractValidator<OrderPutDTO>
    {
        public OrderPutValidator()
        {
            RuleFor(o => o.ProductId).NotNull();
            RuleFor(o => o.AddressInformationId).NotNull();
            RuleFor(o => o.Color).NotNull().MaximumLength(255);
            RuleFor(o => o.TotalPrice).NotNull();
        }
    }
}
