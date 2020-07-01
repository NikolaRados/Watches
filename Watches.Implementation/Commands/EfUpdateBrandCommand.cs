using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.DataTransfer;
using Watches.Application.Exceptions;
using Watches.DataAccess;
using Watches.Domain;
using Watches.Implementation.Validators;

namespace Watches.Implementation.Commands
{
    public class EfUpdateBrandCommand : IUpdateBrandCommand
    {
        private readonly WatchesContext _context;
        private readonly UpdateBrandValidator _validator;

        public EfUpdateBrandCommand(WatchesContext context, UpdateBrandValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Updating brand";

        public void Execute(int id, BrandDto dto)
        {
            dto.Id = id;

            var brand = _context.Brands.Find(id);

            if (brand == null)
            {
                throw new EntityNotFoundException(id, typeof(Brand));
            }

            _validator.ValidateAndThrow(dto);

            brand.Name = dto.Name;

            _context.SaveChanges();
        }
    }
}
