using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.DataTransfer;
using Watches.Application.Searches;

namespace Watches.Application.Queries
{
    public interface IGetOrdersQuery : IQuery<OrderSearch, PagedResponse<ReadOrderDto>>
    {
    }
}
