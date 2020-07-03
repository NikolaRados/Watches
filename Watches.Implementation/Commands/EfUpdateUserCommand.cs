using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.DataTransfer;
using Watches.Application.Exceptions;
using Watches.DataAccess;
using Watches.Domain;
using Watches.Implementation.Validators;

namespace Watches.Implementation.Commands
{
    public class EfUpdateUserCommand : IUpdateUserCommand
    {
        private readonly WatchesContext _context;
        private readonly UpdateUserValidator _validator;

        public EfUpdateUserCommand(WatchesContext context, UpdateUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 22;

        public string Name => "Updating users informations.";

        public void Execute(int id, UpdateUserDto dto)
        {
            dto.Id = id;

            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new EntityNotFoundException(id, typeof(User));
            }

            _validator.ValidateAndThrow(dto);

            /*_mapper.Map(dto, user);*/

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Password = dto.Password;
            user.Email = dto.Email;
            user.UserUseCases = dto.UserUseCases.Select(u => new UserUseCase
            {
                UseCaseId = u.UseCaseId
            }).ToList();

            _context.SaveChanges();
        }
    }
}
