using TaxMicroserviceTakeHomeAssesment.Models.DTO.ITaxService;
using TaxMicroserviceTakeHomeAssesment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TaxMicroserviceTakeHomeAssesment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxController : ControllerBase
    {
        private ILogger<TaxController> _logger;
        private TaxServiceResolver _taxServiceResolver;

        public TaxController(TaxServiceResolver taxServiceResolver, ILogger<TaxController> logger)
        {
            _logger = logger;
            _taxServiceResolver = taxServiceResolver;
        }

        [HttpGet("rate/{zip}")]
        public ActionResult GetTaxRate([FromQuery] GetTaxRateRqModel request)
        {
            var responce = _taxServiceResolver.Invoke(TaxServiceType.TaxJar).GetTaxRateAsync(request);
            return Ok(responce.Result);
        }

        [HttpPost("order")]
        public ActionResult GetOrderTax([FromBody] GetOrderTaxRqModel request)
        {
            var responce = _taxServiceResolver.Invoke(TaxServiceType.TaxJar).GetOrderTaxAsync(request);
            return Ok(responce.Result);
        }
    }
}
