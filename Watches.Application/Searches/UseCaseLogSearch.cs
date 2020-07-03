using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application.Queries;

namespace Watches.Application.Searches
{
    public class UseCaseLogSearch : PagedSearch
    {
        public string Actor { get; set; }
        public string UseCaseName { get; set; }
    }
}
