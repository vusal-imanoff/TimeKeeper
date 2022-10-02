using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.SizeDTO
{
    public class SizePostDTO
    {
        public string Name { get; set; }
    }

    public class SizePostValidator : AbstractValidator<SizePostDTO>
    {
        public SizePostValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        }
    }
}
