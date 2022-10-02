﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.CategoryDTO
{
    public class CategoryPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryPutValidator : AbstractValidator<CategoryPutDTO>
    {
        public CategoryPutValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
        }
    }
}
