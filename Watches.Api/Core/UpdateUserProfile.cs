using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watches.Application.DataTransfer;
using Watches.Domain;

namespace Watches.Api.Core
{
    public class UpdateUserProfile : Profile
    {
        public UpdateUserProfile()
        {
            CreateMap<User, UpdateUserDto>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
