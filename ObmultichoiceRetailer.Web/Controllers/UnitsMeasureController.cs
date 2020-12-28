using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObmultichoiceRetailer.Domain.DomainModels;

namespace ObmultichoiceRetailer.Web.Controllers
{
    [Route("api/unitsmeasure")]
    [ApiController]
    public class UnitsMeasureController : Controller
    {
        private readonly ILogger<UnitsMeasureController> _logger;

        public UnitsMeasureController(ILogger<UnitsMeasureController> logger)
        {
            _logger = logger;
        }
        // GET
        [HttpGet]
        public IActionResult Get()
        {
            var measures = Enum.GetNames(typeof(UnitMeasure)).Select(x => new {
                enumName = x
            }).ToArray();
            return Ok(measures);
        }
    }
}
