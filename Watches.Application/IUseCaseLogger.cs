using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Application
{
    public interface IUseCaseLogger
    {
        void Log(IUseCase useCase, IApplicationActor actor, object UseCaseData);
    }
}
