using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.Exceptions;
using Watches.DataAccess;
using Watches.Domain;

namespace Watches.Implementation.Commands
{
    public class EfDeleteDeleteProductCommand : IDeleteProductCommand
    {
        private readonly WatchesContext _context;

        public EfDeleteDeleteProductCommand(WatchesContext context)
        {
            _context = context;
        }

        public int Id => 6;

        public string Name => "Deleting product.";

        public void Execute(int request)
        {
            var product = _context.Products.Find(request);

            if (product == null)
            {
                throw new EntityNotFoundException(request, typeof(Product));
            }

            product.IsActive = false;
            product.IsDeleted = true;
            product.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
