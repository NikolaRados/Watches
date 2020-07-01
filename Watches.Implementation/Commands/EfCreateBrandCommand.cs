using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.DataTransfer;
using Watches.DataAccess;
using Watches.Domain;
using Watches.Implementation.Validators;

namespace Watches.Implementation.Commands
{
    public class EfCreateBrandCommand : ICreateBrandCommand
    {
        private readonly WatchesContext _context;
        private readonly CreateBrandValidator _validator;

        public EfCreateBrandCommand(WatchesContext context, CreateBrandValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 3;

        public string Name => "Create new Brand";

        public void Execute(BrandDto request)
        {
            _validator.ValidateAndThrow(request);

            var brand = new Brand
            {
                Name = request.Name
            };

            _context.Brands.Add(brand);

            _context.SaveChanges();
        }
    }
}
