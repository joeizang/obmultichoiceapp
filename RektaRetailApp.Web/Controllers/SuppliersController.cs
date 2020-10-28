using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Supplier;
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
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<SupplierApiModel>>> Get()
        {
            var meth = MethodBase.GetCurrentMethod();
            var result = await
                _mediator.Send(
                    new GetAllSuppliersQuery
                    {
                        PageNumber = 1, PageSize = 10,
                        UriName = Url.Link(meth.Name, null)
                    });
            return result;
        }

        // GET api/<SuppliersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SuppliersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SuppliersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
