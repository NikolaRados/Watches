using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application;
using Watches.Application.DataTransfer;
using Watches.Application.Queries;
using Watches.Application.Searches;
using Watches.DataAccess;

namespace Watches.Implementation.Queries
{
    public class EfGetCommentsQuery : IGetCommentsQuery
    {
        private readonly WatchesContext _context;

        public EfGetCommentsQuery(WatchesContext context)
        {
            _context = context;
        }

        public int Id => 16;

        public string Name => "Comments search";

        public PagedResponse<ReadCommentDto> Execute(CommentSearch search)
        {
            var query = _context.Comments
                .Include(x => x.User)
                .Include(o => o.Product).AsQueryable();

            if (!string.IsNullOrEmpty(search.User) || !string.IsNullOrWhiteSpace(search.User))
            {
                query = query.Where(x => x.User.Username.ToLower().Contains(search.User.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.ProductName) || !string.IsNullOrWhiteSpace(search.ProductName))
            {
                query = query.Where(x => x.Product.Name.ToLower().Contains(search.ProductName.ToLower()));
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<ReadCommentDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new ReadCommentDto
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    UserId = x.UserId,
                    ProductName = x.Product.Name,
                    UserName = x.User.Username,
                    Text = x.Text
                }).ToList()
            };
            return response;
        }
    }
}
