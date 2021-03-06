﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.DataAccess;

namespace Watches.Implementation.Validators
{
    public class UpdateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        private readonly WatchesContext _context;
        public UpdateOrderValidator(WatchesContext context)
        {
            _context = context;

            RuleFor(x => x.OrderDate)
                .LessThan(DateTime.Now.AddDays(15))
                .WithMessage("Order day can't be greater than 15 days from today.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.");

            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("There must be atleast one order item.")
                .Must(i => i.Select(x => x.ProductId).Distinct().Count() == i.Count())
                .WithMessage("Duplicate products are not allowed.")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.Items).SetValidator(new UpdateOrderLineValidator(_context));
                });
        }
    }
}
