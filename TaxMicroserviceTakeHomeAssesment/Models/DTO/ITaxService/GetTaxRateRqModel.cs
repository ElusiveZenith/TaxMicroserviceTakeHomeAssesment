using Microsoft.AspNetCore.Mvc;

namespace TaxMicroserviceTakeHomeAssesment.Models.DTO.ITaxService
{
    public class GetTaxRateRqModel
    {
        public string Country { get; set; }

        [FromRoute]
        public string Zip { get; set; }

        public string State { get; set; }
        
        public string City { get; set; }

        public string Street { get; set; }
    }
}
