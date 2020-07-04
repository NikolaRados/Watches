using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.DataTransfer;
using Watches.Application.Email;
using Watches.DataAccess;
using Watches.Domain;
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

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            };

            

            _context.Users.Add(user);
            _context.SaveChanges();

            var a = _context.Users.Where(x => x.Username == request.Username).FirstOrDefault();

            var permits = new List<int> { 4, 10, 16, 18, 8, 1, 17, 14, 15, 19, 21};

            foreach (var permit in permits)
            {
            _context.UserUseCases.Add(new UserUseCase
            {
                UserId = a.Id,
                UseCaseId = permit
            }) ;
            }

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
