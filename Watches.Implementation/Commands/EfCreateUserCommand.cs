using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.DataTransfer;
using Watches.Application.Email;
using Watches.DataAccess;
using Watches.Implementation.Validators;

namespace Watches.Implementation.Commands
{
    
    /*private readonly IEmailSender _sender;*/

    public class EfCreateUserCommand : ICreateUserCommand
    {
        private readonly WatchesContext _context;
        private readonly CreateUserValidator _validator;
        private readonly IEmailSender _sender;


        public EfCreateUserCommand(WatchesContext context, CreateUserValidator validator, IEmailSender sender)
        {
            _context = context;
            _validator = validator;
            _sender = sender;
        }

        public int Id => 21;

        public string Name => "User Registration";

        public void Execute(UserDto request)
        {
            
            _validator.ValidateAndThrow(request);

            _context.Users.Add(new Domain.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            });

            

            _context.SaveChanges();

            _sender.Send(new SendEmailDto
            {
                Content = "<h1>Successful registration!</h1>",
                SendTo = request.Email,
                Subject = "Registration"
            });
        }
    }
}
