using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.DataAccess;

namespace Watches.Implementation.Validators
{
    public class CreateUserValidator : AbstractValidator<UserDto>
    {
        private readonly WatchesContext _context;

        public CreateUserValidator(WatchesContext context)
        {
            _context = context;

            RuleFor(x => x.FirstName).NotEmpty()
                .WithMessage("FirstName is required parameter.")
                .MaximumLength(30)
                .WithMessage("FirstName can't be longer than 30 letters.");

            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage("LastName is required parameter.")
                .MaximumLength(30)
                .WithMessage("LastName can't be longer than 30 letters."); ;

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password is required parameter.")
                .MinimumLength(6)
                .WithMessage("Password can't be shorter than 6 letters.");

            RuleFor(x => x.Username).NotEmpty()
                .WithMessage("Username is required parameter.")
                .MinimumLength(4)
                .WithMessage("Username can't be shorter than 4 letters.")
                .MaximumLength(30)
                .WithMessage("Username  can't be longer than 30 letters.")
                .Must((dto, u) => !_context.Users.Any(x => x.Username == u && x.Id != dto.Id))
                .WithMessage("Username is already taken.");

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email is required parameter.")
                .EmailAddress()
                .WithMessage("Email must be in good format.")
                .Must(u => !_context.Users.Any(x => x.Email == u))
                .WithMessage("Email is already taken.");

            //.Must((dto, name) => !_context.Products.Any(p => p.Name == name && p.Id != dto.Id))
        }
    }
}
