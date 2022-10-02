using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.AccountDTO
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(a => a.Username).NotEmpty().MaximumLength(20).MinimumLength(8);
            RuleFor(a => a.Name).NotEmpty().MaximumLength(40);
            RuleFor(a => a.Surname).NotEmpty().MaximumLength(40);
            RuleFor(a => a.Password).NotEmpty().MinimumLength(8);
            RuleFor(a => a.Email).EmailAddress().NotEmpty();
            RuleFor(a => a.PhoneNumber).NotEmpty();
        }
    }
}
