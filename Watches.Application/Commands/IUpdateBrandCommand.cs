using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.DataTransfer;

namespace Watches.Application.Commands
{
    public interface IUpdateBrandCommand : ICommand<int, BrandDto>
    {
    }
}
