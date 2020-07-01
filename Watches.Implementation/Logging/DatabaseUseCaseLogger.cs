using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Watches.Application;
using Watches.DataAccess;

namespace Watches.Implementation.Logging
{
    public class DatabaseUseCaseLogger : IUseCaseLogger
    {
        private readonly WatchesContext _context;

        public DatabaseUseCaseLogger(WatchesContext context)
        {
            _context = context;
        }

        public void Log(IUseCase useCase, IApplicationActor actor, object UseCaseData)
        {
            _context.UseCaseLogs.Add(new Domain.UseCaseLog
            { 
                Actor = actor.Identity,
                Date = DateTime.UtcNow,
                UseCaseName = useCase.Name,
                Data = JsonConvert.SerializeObject(UseCaseData)
            });

            _context.SaveChanges();
        }
    }
}
