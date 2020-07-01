﻿using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application;
using Watches.Application.Commands;
using Watches.Application.DataTransfer;
using Watches.DataAccess;
using Watches.Domain;
using Watches.Implementation.Validators;

namespace Watches.Implementation.Commands
{
    public class EfCreateOrderCommand : ICreateOrderCommand
    {
        private readonly WatchesContext _context;
        private readonly CreateOrderValidator _validator;
        private readonly IApplicationActor _actor;

        public EfCreateOrderCommand(WatchesContext context, CreateOrderValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }
        public int Id => 14;

        public string Name => "Create new order";

        public void Execute(CreateOrderDto dto)
        {
            _validator.ValidateAndThrow(dto);

            var userId = _actor.Id;

            var order = new Order
            {
                UserId = userId,
                Address = dto.Address,
                OrderDate = dto.OrderDate
            };

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


            _context.Orders.Add(order);

            _context.SaveChanges();
        }
    }
}
