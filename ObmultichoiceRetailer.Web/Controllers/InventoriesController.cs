using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Inventory;
using ObmultichoiceRetailer.Web.Commands.Inventory;
using ObmultichoiceRetailer.Web.Queries.Inventory;

namespace ObmultichoiceRetailer.Web.Controllers
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
        public async Task<ActionResult<PaginatedResponse<InventoryApiModel>>> Get([FromQuery] GetAllInventoriesQuery query)
        {
            var result = await _mediator.Send(query).ConfigureAwait(false);
            if(!result.CurrentResponseStatus.Equals(ResponseStatus.Success))
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "InventoryById")]
        public async Task<ActionResult<InventoryApiModel>> GetInventoryById(GetInventoryDetailQuery model)
        {
            var result = await _mediator.Send(model).ConfigureAwait(false);

            if (result.CurrentResponseStatus == ResponseStatus.Error)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost(Name = "createInventory")]
        public async Task<ActionResult<InventoryApiModel>> CreateInventory([FromBody] CreateInventoryCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);

            if (result.CurrentResponseStatus == ResponseStatus.Success)
                return BadRequest(result);
            return CreatedAtRoute("InventoryById",
                new { id = result.Data.Id }, result);
        }

        [HttpPut]
        public async Task<ActionResult<InventoryApiModel>> UpdateInventory([FromBody] UpdateInventoryCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtRoute("InventoryById",
                new { id = command.InventoryId }, result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInventory([FromRoute] DeleteInventoryCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
