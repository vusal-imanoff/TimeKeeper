using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Service.Extentions;

namespace TimeKeeperFinal.Service.DTOs.BlogDTO
{
    public class BlogPutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainBlog { get; set; }
        public string SubBlog { get; set; }
        public IFormFile File { get; set; }
    }

    public class BlogPutValidator : AbstractValidator<BlogPutDTO>
    {
        public BlogPutValidator()
        {
            RuleFor(b => b.Name).NotEmpty().MaximumLength(255);
            RuleFor(b => b.MainBlog).NotEmpty().MaximumLength(1000);
            RuleFor(b => b.SubBlog).NotEmpty().MaximumLength(1000);
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
