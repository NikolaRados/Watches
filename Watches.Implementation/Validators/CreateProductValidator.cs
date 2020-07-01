using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.DataAccess;
using Watches.Domain;

namespace Watches.Implementation.Validators
{
    public class CreateProductValidator : AbstractValidator<ProductDto>
    {
        private readonly WatchesContext _context;

        public CreateProductValidator(WatchesContext context)
        {
            _context = context;

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Name is required parameter.")
                .Must(name => !_context.Products.Any(p => p.Name == name))
                .WithMessage(p => $"Product with the name of {p.Name} already exists.");

            RuleFor(x => x.Gender).NotEmpty()
                .WithMessage("Gender is required parameter.").IsEnumName(typeof(Gender), caseSensitive: false)
                .WithMessage("Gender must be male or female.");

            RuleFor(x => x.Movement).NotEmpty()
                .WithMessage("Movement is required parameter.").IsEnumName(typeof(Movement), caseSensitive: false)
                .WithMessage("Movement must be quartz, mechanical or automatic.");

            RuleFor(x => x.Glass).NotEmpty()
                .WithMessage("Glass is required parameter.").IsEnumName(typeof(Glass), caseSensitive: false)
                .WithMessage("Glass must be acrylic, mineral or sapphire.");

            RuleFor(x => x.BrandId).Must(x => _context.Brands.Select(b => b.Id).Contains(x))
                .WithMessage(p => $"Group with id of {p.Id} doesn't exists.");

            RuleFor(x => x.Description).NotEmpty()
                .WithMessage("Description is required parameter.");

            RuleFor(x => x.Quantity).GreaterThan(0)
                .WithMessage("Quantity must be above 0.");

            RuleFor(x => x.Price).GreaterThan(0)
                .WithMessage("Price must be above 0.");
        }
    }
}