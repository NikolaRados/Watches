using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Watches.Application.Email;

namespace Watches.Implementation.Email
{
    public class SmptEmailSender : IEmailSender
    {
        public void Send(SendEmailDto dto)
        {
            var smpt = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("nikolaasp2020@gmail.com", "!nikola123")
            };

            var message = new MailMessage("nikolaasp2020@gmail.com", dto.SendTo);
            message.Subject = dto.Subject;
            message.Body = dto.Content;
            message.IsBodyHtml = true;
            smpt.Send(message);
        }
    }
}
