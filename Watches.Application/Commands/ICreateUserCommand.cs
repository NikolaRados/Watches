using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.DataTransfer;

namespace Watches.Application.Commands
{
    public interface ICreateUserCommand : ICommand<UserDto>
    {
    }
}
