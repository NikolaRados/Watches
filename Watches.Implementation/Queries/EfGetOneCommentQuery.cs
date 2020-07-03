using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.Application.Exceptions;
using Watches.Application.Queries;
using Watches.DataAccess;
using Watches.Domain;

namespace Watches.Implementation.Queries
{
    

    public class EfGetOneCommentQuery : IGetOneCommentQuery
    {
        private readonly WatchesContext _context;

        public EfGetOneCommentQuery(WatchesContext context)
        {
            _context = context;
        }

        public int Id => 18;

        public string Name => "Get one comment by Id.";

        public CommentDto Execute(int id)
        {
            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                throw new EntityNotFoundException(id, typeof(Comment));
            }

            var dto = new CommentDto
            {
                Id = id,
                Text = comment.Text,
                ProductId = comment.ProductId,
                UserId = comment.UserId
            };

            return dto;
        }
    }
}
