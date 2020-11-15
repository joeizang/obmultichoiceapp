using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObmultichoiceRetailer.Web.ApiModel.Category;
using ObmultichoiceRetailer.Web.Commands.Category;
using ObmultichoiceRetailer.Web.Queries.Category;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObmultichoiceRetailer.Web.Controllers
{
  [Route("api/categories")]
  [ApiController]
  public class CategoriesController : ControllerBase
  {
    private readonly ILogger<CategoriesController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public CategoriesController(ILogger<CategoriesController> logger,
        IMediator mediator, IMapper mapper)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    // GET: api/<CategoriesController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var result = await _mediator.Send(new GetCategoriesQuery());
      return Ok(result);
    }

    // GET api/<CategoriesController>/5
    [HttpGet("{id}", Name = "GetCategory")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
      var result = await _mediator.Send(new GetCategoryDetailQuery(id));
      return Ok(result);
    }

    // POST api/<CategoriesController>
    [HttpPost(Name = "create")]
    public async Task<IActionResult> Post([FromBody] CreateCategoryApiModel model)
    {
      try
      {
        var command = _mapper.Map<CreateCategoryCommand>(model);
        var result = await _mediator.Send(command);
        return CreatedAtRoute("GetCategory", new { id = result.CategoryId },
            result);
      }
      catch (Exception e)
      {
        return BadRequest(new { e.Message, e.InnerException });
      }
    }

    // PUT api/<CategoriesController>/5
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateCategoryCommand model)
    {
      try
      {
        var result = await _mediator.Send(model);
        return CreatedAtAction("GetCategoryById", new { id = model.Id }, result);
      }
      catch (ArgumentException e)
      {
        return BadRequest(new { e.Message, customMessage = "data submitted is not in a valid state" });
      }
    }

    // DELETE api/<CategoriesController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(DeleteCategoryCommand command)
    {
        var message = string.Empty;
        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            message = e.Message;
        }
        finally
        {
            _logger.Log(LogLevel.Critical, message);
        }
        return BadRequest();
    }
  }
}
