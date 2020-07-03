using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.DataTransfer;
using Watches.DataAccess;
using Watches.Domain;
using Watches.Implementation.Validators;

namespace Watches.Implementation.Commands
{
    public class EfCreateCommentCommand : ICreateCommentCommand
    {

        private readonly WatchesContext _context;
        private readonly CreateCommentValidator _validator;


        public EfCreateCommentCommand(WatchesContext context, CreateCommentValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 17;

        public string Name => "Creating comment";

        public void Execute(CommentDto request)
        {

            _validator.ValidateAndThrow(request);

            var comment = new Comment
            {
                Text = request.Text,
                ProductId = request.ProductId,
                UserId = request.UserId
            };

            _context.Comments.Add(comment);

            _context.SaveChanges();
        }
    }
}
