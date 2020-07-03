using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.Application.Queries;
using Watches.Application.Searches;
using Watches.DataAccess;

namespace Watches.Implementation.Queries
{
    public class EfGetProductsQuery : IGetProductsQuery
    {
        private readonly WatchesContext _context;

        public EfGetProductsQuery(WatchesContext context)
        {
            _context = context;
        }

        public int Id => 1;

        public string Name => "Products search";

        public PagedResponse<ProductDto> Execute(ProductSearch search)
        {
            var query = _context.Products.AsQueryable();

            if(!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<ProductDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new ProductDto
                {
                    Id = x.Id,
                    BrandId = x.BrandId,
                    Description = x.Description,
                    Gender = x.Gender.ToString(),
                    Glass = x.Glass.ToString(),
                    Movement = x.Movement.ToString(),
                    Name = x.Name,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Comments = x.Comments.Select(co => new CommentDto
                    {
                        Id = co.Id,
                        Text = co.Text,
                        UserId = co.UserId,
                        ProductId = co.ProductId
                    }).ToList()
                }).ToList()
            };
            return response;
        }
    }
}
