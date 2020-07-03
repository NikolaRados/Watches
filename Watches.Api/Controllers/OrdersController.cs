using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Watches.Application;
using Watches.Application.Commands;
using Watches.Application.DataTransfer;
using Watches.Application.Queries;
using Watches.Application.Searches;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Watches.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public OrdersController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public IActionResult Get([FromBody] OrderSearch search,
            [FromServices] IGetOrdersQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id,
            [FromServices] IGetOneOrderQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderDto dto,
            [FromServices] ICreateOrderCommand command)
        {
            dto.Id = actor.Id;
            executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CreateOrderDto dto,
            [FromServices] IUpdateOrderCommand command)
        {
            executor.ExecuteCommand(command, id, dto);
            return NoContent();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id,
            [FromServices] IDeleteOrderCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
