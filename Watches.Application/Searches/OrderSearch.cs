using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Queries;

namespace Watches.Application.Searches
{
    public class OrderSearch : PagedSearch
    {
        public int? MinOrderLines { get; set; }
        public decimal? MinPrice { get; set; }
        public string Username { get; set; }
    }
}
