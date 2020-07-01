using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.Exceptions;
using Watches.DataAccess;
using Watches.Domain;

namespace Watches.Implementation.Commands
{
    public class EfDeleteBrandCommand : IDeleteBrandCommand
    {
        private readonly WatchesContext _context;

        public EfDeleteBrandCommand(WatchesContext context)
        {
            _context = context;
        }

        public int Id => 5;

        public string Name => "Deleting brand.";

        public void Execute(int request)
        {
            var brand = _context.Brands.Find(request);

            if(brand == null)
            {
                throw new EntityNotFoundException(request, typeof(Brand));
            }

            brand.IsActive = false;
            brand.IsDeleted = true;
            brand.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
