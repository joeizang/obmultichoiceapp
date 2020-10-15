using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.Queries.Inventory;

namespace RektaRetailApp.Web.Controllers
{
    [Route("api/inventories")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InventoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventories(GetAllInventoriesQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }
    }
}
