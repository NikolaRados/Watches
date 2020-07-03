using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.Exceptions;
using Watches.DataAccess;
using Watches.Domain;

namespace Watches.Implementation.Commands
{
    public class EfDeleteUserCommand : IDeleteUserCommand
    {
        private readonly WatchesContext _context;

        public EfDeleteUserCommand(WatchesContext context)
        {
            _context = context;
        }
        public int Id => 23;

        public string Name => "Deleting user.";

        public void Execute(int request)
        {
            var user = _context.Users.Find(request);

            if (user == null)
            {
                throw new EntityNotFoundException(request, typeof(User));
            }

            user.IsActive = false;
            user.IsDeleted = true;
            user.DeletedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
