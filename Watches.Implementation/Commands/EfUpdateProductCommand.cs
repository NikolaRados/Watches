using AutoMapper;
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
    public class EfUpdateProductCommand : IUpdateProductCommand
    {
        private readonly WatchesContext _context;
        private readonly UpdateProductValidator _validator;
        private readonly IMapper _mapper;

        public EfUpdateProductCommand(WatchesContext context, UpdateProductValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 7;

        public string Name => "Updating product.";

        public void Execute(int id, ProductDto dto)
        {
            dto.Id = id;

            var product = _context.Products.Find(id);

            if (product == null)
            {
                throw new EntityNotFoundException(id, typeof(Product));
            }

            _validator.ValidateAndThrow(dto);

            _mapper.Map(dto, product);

            _context.SaveChanges();
        }
    }
}
