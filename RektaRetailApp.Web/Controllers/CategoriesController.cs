using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Category;
using RektaRetailApp.Web.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RektaRetailApp.Web.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryRepository _repo;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoryRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repo.GetCategories().ConfigureAwait(false);
            return Ok(result);
        }

        //[HttpGet]
        public async Task<IActionResult> GetCategoryDropDown()
        {
            var dropDown = await _repo.GetForDropDown();

            if (dropDown == null)
            {
                return BadRequest();
            }

            return Ok(dropDown);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await _repo.GetCategoryById(id));
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryApiModel model)
        {
            try
            {
                _repo.Create(model);
                await _repo.Commit().ConfigureAwait(false);
                var response = await _repo.GetCategoryBy(model.Name, model.Description);
                return CreatedAtRoute("GetCategory", new { id = response.CategoryId }, response);
            }
            catch (ArgumentException)
            {
                return BadRequest("Category creation was unsuccessful. You object was in an invalid state!");
            }
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
