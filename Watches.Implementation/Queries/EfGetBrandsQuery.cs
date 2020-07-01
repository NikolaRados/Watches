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
    public class EfGetBrandsQuery : IGetBrandsQuery
    {
        private readonly WatchesContext _context;

        public EfGetBrandsQuery(WatchesContext context)
        {
            _context = context;
        }

        public int Id => 4;

        public string Name => "Brand search";

        public PagedResponse<BrandDto> Execute(BrandSearch search)
        {
            var query = _context.Brands.AsQueryable();


            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }


            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<BrandDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new BrandDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return response;
        }
    }
}
