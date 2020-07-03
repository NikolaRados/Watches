using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.Exceptions;
using Watches.DataAccess;
using Watches.Domain;

namespace Watches.Implementation.Commands
{
    public class EfDeleteCommentCommand : IDeleteCommentCommand
    {
        private readonly WatchesContext _context;

        public EfDeleteCommentCommand(WatchesContext context)
        {
            _context = context;
        }
        public int Id => 20;

        public string Name => "Deleting comment";

        public void Execute(int request)
        {
            var comment = _context.Comments.Find(request);

            if (comment == null)
            {
                throw new EntityNotFoundException(request, typeof(Comment));
            }

            comment.IsActive = false;
            comment.IsDeleted = true;
            comment.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
