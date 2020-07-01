using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.DataAccess;
using Watches.Domain;

namespace Watches.Implementation.Validators
{
    public class CreateBrandValidator : AbstractValidator<BrandDto>
    {
        private readonly WatchesContext _context;

        public CreateBrandValidator(WatchesContext context)
        {
            _context = context;

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Brand name is required.")
                .Must(name => !_context.Brands.Any(x => x.Name == name))
                .WithMessage("Brand name must be unique.");
        }
    }
}
