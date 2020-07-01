using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watches.Application.DataTransfer;
using Watches.Domain;

namespace Watches.Api.Core
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dto => dto.Gender, opt => opt.MapFrom(product => product.Gender.ToString()))
                .ForMember(dto => dto.Movement, opt => opt.MapFrom(product => product.Movement.ToString()))
                .ForMember(dto => dto.Glass, opt => opt.MapFrom(product => product.Glass.ToString()));

            CreateMap<ProductDto, Product>()
                .ForMember(product => product.Gender, opt => opt.MapFrom(dto => (Gender)Enum.Parse(typeof(Gender), dto.Gender, true)))
                .ForMember(product => product.Movement, opt => opt.MapFrom(dto => (Movement)Enum.Parse(typeof(Movement), dto.Movement, true)))
                .ForMember(product => product.Glass, opt => opt.MapFrom(dto => (Glass)Enum.Parse(typeof(Glass), dto.Glass, true)));
        }
    }
}
