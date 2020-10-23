using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Inventory;
using RektaRetailApp.Web.Commands.Inventory;
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
    public async Task<ActionResult<IEnumerable<InventoryApiModel>>> Get()
    {
      var query = new GetAllInventoriesQuery();
      var result = await _mediator.Send(query);
      return Ok(result);
    }

    [HttpGet("{id}", Name = "InventoryById")]
    public async Task<ActionResult<InventoryApiModel>> GetInventoryById(GetInventoryDetailQuery model)
    {
      var result = await _mediator.Send(model);
      return Ok(result);
    }

    [HttpPost(Name = "createInventory")]
    public async Task<ActionResult<InventoryApiModel>> CreateInventory([FromBody] CreateInventoryCommand command)
    {
      var result = await _mediator.Send(command);
      return CreatedAtRoute("InventoryById", 
          new { id = result.Id } ,result);
    }
  }
}
