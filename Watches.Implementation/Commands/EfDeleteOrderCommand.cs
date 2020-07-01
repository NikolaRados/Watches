using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.Exceptions;
using Watches.DataAccess;
using Watches.Domain;

namespace Watches.Implementation.Commands
{
    public class EfDeleteOrderCommand : IDeleteOrderCommand
    {
        private readonly WatchesContext _context;

        public EfDeleteOrderCommand(WatchesContext context)
        {
            _context = context;
        }
        public int Id => 13;

        public string Name => "Deleting order";

        public void Execute(int request)
        {
            var order = _context.Products.Find(request);

            if (order == null)
            {
                throw new EntityNotFoundException(request, typeof(Order));
            }

            order.IsActive = false;
            order.IsDeleted = true;
            order.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
