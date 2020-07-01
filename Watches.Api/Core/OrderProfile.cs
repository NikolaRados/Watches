using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watches.Application.DataTransfer;
using Watches.Domain;

namespace Watches.Api.Core
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, ReadOrderDto>()
                .ForMember(dto => dto.UserInfo, opt => opt.MapFrom(order => order.User.FirstName + " " + order.User.LastName + " " + order.User.Username))
                .ForMember(dto => dto.OrderLines, opt => opt.MapFrom(order => order.OrderLines.Select(ol => new ReadOrderLineDto
                {
                    Id = ol.Id,
                    Name = ol.Name,
                    Price = ol.Price,
                    Quantity = ol.Quantity
                })));

        }
    }
}
