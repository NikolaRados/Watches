using System;
using System.Collections.Generic;
using System.Text;

namespace Watches.Application
{
    public interface ICommand<TRequest> : IUseCase
    {
        void Execute(TRequest request);
    }

    public interface ICommand<TRequest1, TRequest2> : IUseCase
    {
        void Execute(TRequest1 request1, TRequest2 request2);
    }

    public interface IQuery<TSearch, TResult> : IUseCase
    {
        TResult Execute(TSearch search);
    }

    public interface IUseCase
    {
        int Id { get; }
        string Name { get; }
    }
}
