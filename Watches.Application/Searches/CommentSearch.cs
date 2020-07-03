using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Queries;

namespace Watches.Application.Searches
{
    public class CommentSearch : PagedSearch
    {
        public string User { get; set; }
        public string ProductName { get; set; }
    }
}
