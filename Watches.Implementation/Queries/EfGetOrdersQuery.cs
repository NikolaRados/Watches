using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class EfGetOrdersQuery : IGetOrdersQuery
    {
        private readonly WatchesContext _context;

        public EfGetOrdersQuery(WatchesContext context)
        {
            _context = context;
        }

        public int Id => 11;

        public string Name => "Orders search";

        public PagedResponse<ReadOrderDto> Execute(OrderSearch search)
        {
            var query = _context.Orders
                .Include(x => x.User)
                .Include(o => o.OrderLines).AsQueryable();

            if (!string.IsNullOrEmpty(search.Username) || !string.IsNullOrWhiteSpace(search.Username))
            {
                query = query.Where(x => x.User.Username.ToLower().Contains(search.Username.ToLower()));
            }

            if (search.MinOrderLines.HasValue)
            {
                query = query.Where(x => x.OrderLines.Count >= search.MinOrderLines.Value);
            }

            if (search.MinPrice.HasValue)
            {
                query = query.Where(o => o.OrderLines.Sum(x => x.Quantity * x.Price) >= search.MinPrice.Value);
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<ReadOrderDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new ReadOrderDto
                {
                    Id = x.Id,
                    UserInfo = x.User.FirstName + " " + x.User.LastName + " " + x.User.Username,
                    Address = x.Address,
                    CreatedAt = x.CreatedAt,
                    OrderLines = x.OrderLines.Select(ol => new ReadOrderLineDto
                    {
                        Id = ol.Id,
                        Name = ol.Name,
                        Price = ol.Price,
                        Quantity = ol.Quantity
                    }).ToList()
                }).ToList()
            };

            return response;
        }
    }
}

