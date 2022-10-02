using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TimeKeeperFinal.Core.Entities;
using TimeKeeperFinal.Service.DTOs.AccountDTO;
using TimeKeeperFinal.Service.DTOs.EmailConfiguration;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Service.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IWebHostEnvironment _env;

        public EmailService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task ForgotPassword(AppUser user, string url, ForgotPasswordDTO forgotPassword)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("TimeKeeper", "timekeepertester@gmail.com"));
            message.To.Add(new MailboxAddress(user.Name, forgotPassword.Email));
            message.Subject = "Reset Password";

            string emailbody = string.Empty;

            using (StreamReader streamReader = new StreamReader(Path.Combine(_env.WebRootPath, "html", "reset.html")))
            {
                emailbody = streamReader.ReadToEnd();
            }

            emailbody =  "<a href='[URL]'>Confirmation Link</a>".Replace("[URL]", url);

            using var smtp = new SmtpClient();
            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };


            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("timekeepertester@gmail.com", "sbrdezwsrgsjelyx");
            smtp.Send(message);
            smtp.Disconnect(true);
        }

        public async Task Register(RegisterDTO registerDto, string link)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("TimeKeeper", "timekeepertester@gmail.com"));
            message.To.Add(new MailboxAddress(registerDto.Name, registerDto.Email));
            message.Subject = "Confirm Email";
            
            string emailbody = "<a href='[URL]'>Confirmation Link</a>".Replace("[URL]", link);
            using var smtp = new SmtpClient();
            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("timekeepertester@gmail.com", "sbrdezwsrgsjelyx");
            smtp.Send(message);
            smtp.Disconnect(true);

        }

    }
}
