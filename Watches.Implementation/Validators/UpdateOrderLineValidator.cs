using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.DataAccess;

namespace Watches.Implementation.Validators
{
    public class UpdateOrderLineValidator : AbstractValidator<CreateOrderLineDto>
    {
        private readonly WatchesContext _context;
        public UpdateOrderLineValidator(WatchesContext context)
        {
            _context = context;

            RuleFor(x => x.ProductId)
                .Must(ProductExists)
                .WithMessage(p => $"Product with the id of { p.ProductId } doesn't exist.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Quantity)
                        .GreaterThan(0)
                        .WithMessage("Quantity must be greater than 0.")
                        .Must(QuantityDoesntExeedProductQuantity)
                        .WithMessage("Defined quantity ({PropertyValue}) is unavailable.");
                });
        }

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(x => x.Id == productId);
        }

        public bool QuantityDoesntExeedProductQuantity(CreateOrderLineDto dto, int quantity)
        {
            return _context.Products.Find(dto.ProductId).Quantity >= quantity + dto.Quantity;
        }
    }
}
