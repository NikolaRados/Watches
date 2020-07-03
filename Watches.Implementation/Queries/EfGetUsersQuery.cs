using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.Application.Queries;
using Watches.Application.Searches;
using Watches.DataAccess;
using Watches.Domain;

namespace Watches.Implementation.Queries
{
    public class EfGetUsersQuery : IGetUsersQuery
    {
        private readonly WatchesContext _context;

        public EfGetUsersQuery(WatchesContext context)
        {
            _context = context;
        }

        public int Id => 24;

        public string Name => "Users search";

        public PagedResponse<UserDto> Execute(UserSearch search)
        {
            var query = _context.Users.AsQueryable();


            if (!string.IsNullOrEmpty(search.Username) || !string.IsNullOrWhiteSpace(search.Username))
            {
                query = query.Where(x => x.Username.ToLower().Contains(search.Username.ToLower()));
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<UserDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new UserDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username,
                    Password = x.Password,
                    Email = x.Email,
                    Orders = x.Orders.Select(co => new OrderDto
                    {
                        Id = co.Id,
                        Address = co.Address,
                        OrderDate = co.OrderDate,
                        UserId = co.UserId
                    }).ToList(),
                    UserUseCases = x.UserUseCases.Select(u => new UserUseCaseDto
                    {
                       UseCaseId = u.UseCaseId
                    }).ToList(),
                }).ToList()
            };

            return response;
        }
    }
}
