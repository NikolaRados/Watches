using AutoMapper;
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
    public class EfGetOneBrandQuery : IGetOneBrandQuery
    {
        private readonly WatchesContext _context;
        private readonly IMapper _mapper;

        public EfGetOneBrandQuery(WatchesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 10;

        public string Name => "Search one brand with Id";

        public BrandDto Execute(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
            {
                throw new EntityNotFoundException(id, typeof(Brand));
            }

            var dto = new BrandDto
            {
                Id = id,
                Name = brand.Name
            };

            return dto;
        }
    }
}
