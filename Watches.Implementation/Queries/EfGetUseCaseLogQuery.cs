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
    public class EfGetUseCaseLogQuery : IGetUseCaseLogQuery
    {
        private readonly WatchesContext _context;

        public EfGetUseCaseLogQuery(WatchesContext context)
        {
            _context = context;
        }
        public int Id => 26;

        public string Name => "Search use case log";

        public PagedResponse<UseCaseLogDto> Execute(UseCaseLogSearch search)
        {
            var query = _context.UseCaseLogs.AsQueryable();


            if (!string.IsNullOrEmpty(search.Actor) || !string.IsNullOrWhiteSpace(search.Actor))
            {
                query = query.Where(x => x.Actor.ToLower().Contains(search.Actor.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.UseCaseName) || !string.IsNullOrWhiteSpace(search.UseCaseName))
            {
                query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCaseName.ToLower()));
            }


            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<UseCaseLogDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new UseCaseLogDto
                {
                    Id = x.Id,
                    Actor = x.Actor,
                    Data = x.Data,
                    Date = x.Date,
                    UseCaseName = x.UseCaseName
                }).ToList()
            };

            return response;
        }
    }
}
