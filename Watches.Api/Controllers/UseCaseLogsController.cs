using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Watches.Application;
using Watches.Application.Queries;
using Watches.Application.Searches;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Watches.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UseCaseLogsController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public UseCaseLogsController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        // GET: api/<UseCaseLogsController>
        [HttpGet]
        public IActionResult Get([FromBody] UseCaseLogSearch search,
            [FromServices] IGetUseCaseLogQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }
    }
}
