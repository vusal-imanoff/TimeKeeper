using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.ModelDTO
{
    public class ModelPostDTO
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
    }
    public class ModelPostValidator : AbstractValidator<ModelPostDTO>
    {
        public ModelPostValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
