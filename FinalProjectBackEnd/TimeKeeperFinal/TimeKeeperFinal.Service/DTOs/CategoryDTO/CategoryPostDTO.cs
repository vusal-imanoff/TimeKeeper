﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.CategoryDTO
{
    public class CategoryPostDTO
    {
        public string Name { get; set; }
    }
    public class CategoryPostValidator : AbstractValidator<CategoryPostDTO>
    {
        public CategoryPostValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
