﻿using System;
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
    public class BrandsController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public BrandsController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        // GET: api/<BrandsController>
        [HttpGet]
        public IActionResult Get([FromBody] BrandSearch search,
            [FromServices] IGetBrandsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id,
            [FromServices] IGetOneBrandQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }
        [Authorize]
        // POST api/<BrandsController>
        [HttpPost]
        public IActionResult Post([FromBody] BrandDto dto,
            [FromServices] ICreateBrandCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return NoContent();
        }
        [Authorize]
        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] BrandDto dto,
            [FromServices] IUpdateBrandCommand command)
        {
            executor.ExecuteCommand(command, id, dto);
            return NoContent();
        }
        [Authorize]
        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id,
            [FromServices] IDeleteBrandCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
