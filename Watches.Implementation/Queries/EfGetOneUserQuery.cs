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
    public class EfGetOneUserQuery : IGetOneUserQuery
    {
        private readonly WatchesContext _context;
        private readonly IMapper _mapper;

        public EfGetOneUserQuery(WatchesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 25;

        public string Name => "Search Users with id";

        public UserDto Execute(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new EntityNotFoundException(id, typeof(User));
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
