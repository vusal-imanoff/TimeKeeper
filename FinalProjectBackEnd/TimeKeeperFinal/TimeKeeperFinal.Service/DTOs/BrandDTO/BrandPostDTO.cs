using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Service.Extentions;

namespace TimeKeeperFinal.Service.DTOs.BrandDTO
{
    public class BrandPostDTO
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
    public class BrandPostValidator : AbstractValidator<BrandPostDTO>
    {
        public BrandPostValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
            RuleFor(b => b).Custom((x, context) =>
            {
                if (x.File != null)
                {
                    if (x.File.CheckFileContextType("image/jpeg"))
                    {
                        context.AddFailure("Please Select Correct Image Type. Example Jpeg or Jpg");
                    }
                    if (x.File.CheckFileSize(2000))
                    {
                        context.AddFailure("Please Select Coorect Image Size. Maximum 2 MB");
                    }
                }
            });
        }
    }
}
