using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.ApiModel.AuthManager;
using ObmultichoiceRetailer.Web.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObmultichoiceRetailer.Web.Controllers
{
    [Route("api/authManager")]
    [ApiController]
    public class AuthManager : ControllerBase
    {
        private readonly ILogger<AuthManager> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthManager(ILogger<AuthManager> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        // GET: api/<AuthManager>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthManager>/5
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var result = await _userManager.Users.Include(u => u.SalesYouOwn)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    NumberOfSuccessfulSales = x.SalesYouOwn.Count,
                    x.Id
                }).SingleOrDefaultAsync().ConfigureAwait(false);
            return Ok(result);
        }

        // POST api/<AuthManager>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserInputModel model)
        {
            try
            {
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email
                };
                if (string.IsNullOrEmpty(model.OtherNames))
                {
                    user.OtherNames = string.Empty;
                }
                else
                {
                    user.OtherNames = model.OtherNames!;
                }

                user.EmailConfirmed = true;
                
                await _userManager.CreateAsync(user, model.Password);

                var newUser = await _userManager.FindByEmailAsync(model.Email);
                return CreatedAtRoute("GetUserById",new { newUser.Id },new {newUser.Email, newUser.Id});

            }
            catch (Exception e)
            {
                return BadRequest(new {e.Message});
            }
        }

        // PUT api/<AuthManager>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthManager>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
