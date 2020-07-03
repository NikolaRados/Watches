using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.DataAccess;

namespace Watches.Implementation.Validators
{
    public class UpdateCommentValidator : AbstractValidator<CommentDto>
    {
        private readonly WatchesContext _context;

        public UpdateCommentValidator(WatchesContext context)
        {
            _context = context;

            RuleFor(x => x.Text).NotEmpty()
                .WithMessage("Comment can't exist without any character.");

            RuleFor(x => x.ProductId).Must(x => _context.Products.Select(b => b.Id).Contains(x))
                .WithMessage(p => $"Product with id of {p.ProductId} doesn't exists.");

            RuleFor(x => x.UserId).Must(x => _context.Users.Select(b => b.Id).Contains(x))
                .WithMessage(p => $"Product with id of {p.UserId} doesn't exists.");

        }
    }
}
