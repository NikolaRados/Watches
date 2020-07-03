using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.DataTransfer;
using Watches.Application.Exceptions;
using Watches.DataAccess;
using Watches.Domain;
using Watches.Implementation.Validators;

namespace Watches.Implementation.Commands
{
    public class EfUpdateCommentCommand : IUpdateCommentCommand
    {
        private readonly WatchesContext _context;
        private readonly UpdateCommentValidator _validator;

        public EfUpdateCommentCommand(WatchesContext context, UpdateCommentValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 19;

        public string Name => "Updating comment";

        public void Execute(int id, CommentDto dto)
        {
            dto.Id = id;

            var comment = _context.Comments.Find(id);

            if (comment== null)
            {
                throw new EntityNotFoundException(id, typeof(Comment));
            }

            _validator.ValidateAndThrow(dto);

            comment.Text = dto.Text;
            comment.ProductId = dto.ProductId;
            comment.UserId = dto.UserId;

            _context.SaveChanges();
        }
    }
}
