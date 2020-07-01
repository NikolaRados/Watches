using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.Application.Exceptions;
using Watches.Application.Queries;
using Watches.DataAccess;
using Watches.Domain;

namespace Watches.Implementation.Queries
{
    public class EfGetOneOrderQuery : IGetOneOrderQuery
    {
        private readonly WatchesContext _context;
        private readonly IMapper _mapper;

        public EfGetOneOrderQuery(WatchesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 12;

        public string Name => "Search one order with Id";

        public ReadOrderDto Execute(int id)
        {
            var order = _context.Orders
                .Include(x => x.User)
                .Include(o => o.OrderLines).FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                throw new EntityNotFoundException(id, typeof(Order));
            }

            return _mapper.Map<ReadOrderDto>(order);
        }
    }
}
