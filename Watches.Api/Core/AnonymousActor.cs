using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watches.Application;

namespace Watches.Api.Core
{
    public class AnonymousActor : IApplicationActor
    {
        public int Id => 0;

        public string Identity => "Anonymus";

        public IEnumerable<int> AllowUseCases => Enumerable.Range(1, 1000);
    }
}
