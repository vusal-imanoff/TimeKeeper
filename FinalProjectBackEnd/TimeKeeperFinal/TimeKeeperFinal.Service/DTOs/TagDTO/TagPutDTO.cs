using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.TagDTO
{
    public class TagPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TagPutValidator : AbstractValidator<TagPutDTO>
    {
        public TagPutValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        }
    }
}
