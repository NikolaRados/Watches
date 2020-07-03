using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Queries;

namespace Watches.Application.Searches
{
    public class UserSearch : PagedSearch
    {
        public string  Username { get; set; }
    }
}
