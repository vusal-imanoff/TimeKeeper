using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.TagDTO
{
    public class TagPostDTO
    {
        public string Name { get; set; }
    }

    public class TagPostValidator : AbstractValidator<TagPostDTO>
    {
        public TagPostValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        }
    }
}
