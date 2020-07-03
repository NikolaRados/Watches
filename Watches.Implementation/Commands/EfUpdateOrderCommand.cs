using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
    public class EfUpdateOrderCommand : IUpdateOrderCommand
    {
        private readonly WatchesContext _context;
        private readonly UpdateOrderValidator _validator;

        public EfUpdateOrderCommand(WatchesContext context, UpdateOrderValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 15;

        public string Name => "Updating order";

        public void Execute(int id, CreateOrderDto dto)
        {
            dto.Id = id;

            var order = _context.Orders
                .Include(x => x.User)
                .Include(o => o.OrderLines).FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                throw new EntityNotFoundException(id, typeof(Order));
            }

            _validator.ValidateAndThrow(dto);

            foreach(var or in order.OrderLines)
            {
                var pr = _context.Products.Find(or.ProductId);
                var stari = order.OrderLines.FirstOrDefault(x => x.ProductId == or.ProductId);
                pr.Quantity += stari.Quantity;
                or.IsActive = false;
                or.IsDeleted = true;
                or.DeletedAt = DateTime.Now;
            }


            order.Address = dto.Address;
            order.OrderDate = dto.OrderDate;


            foreach (var item in dto.Items)
            {
                var product = _context.Products.Find(item.ProductId);

                
                product.Quantity -= item.Quantity;

                order.OrderLines.Add(new OrderLine
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Name = product.Name,
                    Price = product.Price
                });
            }
            

            _context.SaveChanges();
        }
    }
}
