using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Watches.Application.Commands;
using Watches.Application.DataTransfer;
using Watches.DataAccess;
using Watches.Domain;
using Watches.Implementation.Validators;

namespace Watches.Implementation.Commands
{
    public class EfCreateProductCommand : ICreateProductCommand
    {
        private readonly WatchesContext _context;
        private readonly CreateProductValidator _validator;
        private readonly IMapper _mapper;

        public EfCreateProductCommand(WatchesContext context, CreateProductValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 2;

        public string Name => "Create new Product.";

        public void Execute(ProductDto request)
        {
            _validator.ValidateAndThrow(request);

            var result = _mapper.Map<Product>(request);


            _context.Products.Add(result);

            _context.SaveChanges();

        }
    }
}
