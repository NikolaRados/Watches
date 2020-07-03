using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watches.Application;

namespace Watches.Api.Core
{
    public class JwtActor : IApplicationActor
    {
        public int Id { get; set; }

        public string Identity { get; set; }

        public IEnumerable<int> AllowUseCases { get; set; }
    }
}
