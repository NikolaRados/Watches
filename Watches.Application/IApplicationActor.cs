using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Application
{
    public interface IApplicationActor
    {
        int Id { get; }
        string Identity { get; }
        IEnumerable<int> AllowUseCases { get; }
    }
}
