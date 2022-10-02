using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Service.Extentions;

namespace TimeKeeperFinal.Service.DTOs.SliderDTO
{
    public class SliderPutDTO
    {
        public int Id { get; set; }
        public string MainTitle { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }
    public class SliderPutValidator : AbstractValidator<SliderPutDTO>
    {
        public SliderPutValidator()
        {
            RuleFor(b => b.MainTitle).NotEmpty().MaximumLength(255);
            RuleFor(b => b.SubTitle).NotEmpty().MaximumLength(255);
            RuleFor(b => b.Description).NotEmpty().MaximumLength(255);
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
