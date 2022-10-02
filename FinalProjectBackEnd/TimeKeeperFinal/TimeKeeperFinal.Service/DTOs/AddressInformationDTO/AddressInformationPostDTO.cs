using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.AddressInformationDTO
{
    public class AddressInformationPostDTO
    {
        public string AppUserId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }

    public class AddressInformationPostValidator:AbstractValidator<AddressInformationPostDTO>
    {
        public AddressInformationPostValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MaximumLength(255);
            RuleFor(a => a.SurName).NotEmpty().MaximumLength(255);
            RuleFor(a => a.Email).NotEmpty().EmailAddress();
            RuleFor(a => a.Phone).NotEmpty().MaximumLength(255);
            RuleFor(a => a.Address).NotEmpty().MaximumLength(255);
            RuleFor(a => a.Country).NotEmpty().MaximumLength(255);
            RuleFor(a => a.City).NotEmpty().MaximumLength(255);
            RuleFor(a => a.ZipCode).NotEmpty().MaximumLength(255);
        }
    }
}
