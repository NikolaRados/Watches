using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.DataAccess;

namespace Watches.Implementation.Validators
{
    public class UpdateBrandValidator : AbstractValidator<BrandDto>
    {
        private readonly WatchesContext _context;

        public UpdateBrandValidator(WatchesContext context)
        {
            _context = context;

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Brand name is required.")
                .Must((dto, name) => !_context.Brands.Any(x => x.Name == name && x.Id != dto.Id ))
                .WithMessage("Brand name must be unique.");
        }
    }
}
