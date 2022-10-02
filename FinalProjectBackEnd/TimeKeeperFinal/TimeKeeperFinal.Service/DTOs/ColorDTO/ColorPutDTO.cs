using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.ColorDTO
{
    public class ColorPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class ColorPutValidator : AbstractValidator<ColorPutDTO>
    {
        public ColorPutValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
            RuleFor(b => b.Code).NotEmpty().MaximumLength(255);
        }
    }
}
