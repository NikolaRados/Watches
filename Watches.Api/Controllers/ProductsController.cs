﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ProductsController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public ProductsController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }





        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get([FromBody] ProductSearch search,
            [FromServices] IGetProductsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id,
            [FromServices] IGetOneProductQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }
        [Authorize]
        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductDto dto,
            [FromServices] ICreateProductCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return NoContent();
        }
        [Authorize]
        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductDto dto,
            [FromServices] IUpdateProductCommand command)
        {
            executor.ExecuteCommand(command, id, dto);
            return NoContent();
        }
        [Authorize]
        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id,
            [FromServices] IDeleteProductCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
