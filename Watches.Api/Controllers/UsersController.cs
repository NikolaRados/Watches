using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Watches.Application;
using Watches.Application.Commands;
using Watches.Application.DataTransfer;
using Watches.Application.Email;
using Watches.Application.Queries;
using Watches.Application.Searches;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Watches.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public UsersController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }
        [Authorize]
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get([FromBody] UserSearch search,
            [FromServices] IGetUsersQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }
        [Authorize]
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id,
            [FromServices] IGetOneUserQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult Post([FromBody] UserDto dto,
            [FromServices] ICreateUserCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return NoContent();
        }
        [Authorize]
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UpdateUserDto dto,
            [FromServices] IUpdateUserCommand command)
        {
            executor.ExecuteCommand(command, id, dto);
            return NoContent();
        }
        [Authorize]
        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id,
            [FromServices] IDeleteUserCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
