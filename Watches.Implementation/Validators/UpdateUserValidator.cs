using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.DataAccess;

namespace Watches.Implementation.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        private readonly WatchesContext _context;

        public UpdateUserValidator(WatchesContext context)
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
                .Must(u => !_context.Users.Any(x => x.Username == u))
                .WithMessage("Username is already taken.");

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email is required parameter.")
                .EmailAddress()
                .WithMessage("Email must be in good format.");

            RuleFor(x => x.UserUseCases)
                .NotEmpty().WithMessage("User needs atleast one use case")
                .Must(i => i.Select(x => x.UseCaseId).Distinct().Count() == i.Count())
                .WithMessage("Duplicate products are not allowed.");
        }
    }
}
