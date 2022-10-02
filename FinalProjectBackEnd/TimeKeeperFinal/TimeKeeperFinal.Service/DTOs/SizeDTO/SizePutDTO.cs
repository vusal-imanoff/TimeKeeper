using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.SizeDTO
{
    public class SizePutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SizePutValidator : AbstractValidator<SizePutDTO>
    {
        public SizePutValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        }
    }
}
