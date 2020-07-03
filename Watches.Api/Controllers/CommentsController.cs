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
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IApplicationActor _actor;
        private readonly UseCaseExecutor _executor;

        public CommentsController(IApplicationActor actor, UseCaseExecutor executor)
        {
            _actor = actor;
            _executor = executor;
        }



        // GET: api/<CommentsController>
        [HttpGet]
        public IActionResult Get([FromBody] CommentSearch search,
            [FromServices] IGetCommentsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id,
            [FromServices] IGetOneCommentQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }
        [Authorize]
        // POST api/<CommentsController>
        [HttpPost]
        public IActionResult Post([FromBody] CommentDto dto,
            [FromServices] ICreateCommentCommand command)
        {
            dto.UserId = _actor.Id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }
        [Authorize]
        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CommentDto dto,
            [FromServices] IUpdateCommentCommand command)
        {
            _executor.ExecuteCommand(command, id, dto);
            return NoContent();
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id,
            [FromServices] IDeleteCommentCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
