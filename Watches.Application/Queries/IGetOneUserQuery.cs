using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.DataTransfer;

namespace Watches.Application.Queries
{
    public interface IGetOneUserQuery : IQuery<int, UserDto>
    {
    }
}
