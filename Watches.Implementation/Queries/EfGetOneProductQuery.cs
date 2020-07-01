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
    public class EfGetOneProductQuery : IGetOneProductQuery
    {
        private readonly WatchesContext _context;
        private readonly IMapper _mapper;

        public EfGetOneProductQuery(WatchesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 8;

        public string Name => "Search one product with Id";

        public ProductDto Execute(int search)
        {
            var product = _context.Products.Find(search);

            if (product == null)
            {
                throw new EntityNotFoundException(search, typeof(Product));
            }

            return _mapper.Map<ProductDto>(product);
        }
    }
}
