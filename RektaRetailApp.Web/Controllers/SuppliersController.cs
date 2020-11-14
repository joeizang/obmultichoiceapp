using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.Commands.Supplier;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.Queries.Supplier;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RektaRetailApp.Web.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<SuppliersController>
        [HttpGet(Name = "GetAllSuppliers")]
        public async Task<ActionResult<PaginatedResponse<SupplierApiModel>>> GetSuppliers([FromQuery]GetAllSuppliersQuery? query)
        {
            if (query != null)
            {
                query.UriName = Url.Link("GetAllSuppliers", new PaginatedMetaData());
                var result = await _mediator.Send(query);
                return Ok(result);
            }

            query = new GetAllSuppliersQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SearchTerm = string.Empty,
                UriName = Url.Link("GetAllSuppliers", new PaginatedMetaData())
            };
            var res = await _mediator.Send(query).ConfigureAwait(false);
            return Ok(res);
        }

        // GET api/<SuppliersController>/5
        [HttpGet("{id}", Name = "GetSupplierById")]
        public async Task<ActionResult<Response<SupplierDetailApiModel>>> GetSupplierById(int id)
        {
            var result = await _mediator.Send(new GetSupplierByIdQuery {Id = id});
            return Ok(result);
        }

        // POST api/<SuppliersController>
        [HttpPost]
        public async Task<ActionResult<Response<SupplierApiModel>>> Post(CreateSupplierCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetSupplierById", new { id = result.Data.SupplierId },result);
        }

        // PUT api/<SuppliersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<SupplierDetailApiModel>>> Put(int id, [FromBody] UpdateSupplierCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Created("GetSupplierById", result);
        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteSupplierCommand {Id = id};

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
