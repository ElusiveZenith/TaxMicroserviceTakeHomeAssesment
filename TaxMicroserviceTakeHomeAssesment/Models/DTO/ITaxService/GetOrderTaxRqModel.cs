using System.Collections.Generic;

namespace TaxMicroserviceTakeHomeAssesment.Models.DTO.ITaxService
{
    public class GetOrderTaxRqModel
    {
        public string FromCountry { get; set; }
        
        public string FromZip { get; set; }

        public string FromState { get; set; }
        
        public string FromCity { get; set; }

        public string FromStreet { get; set; }

        public string ToCountry { get; set; }

        public string ToZip { get; set; }

        public string ToState { get; set; }

        public string ToCity { get; set; }

        public string ToStreet { get; set; }

        public float Amount { get; set; }

        public float ShippingAmmount { get; set; }

        public string ExemptionType { get; set; }
        
        public List<GetOrderTaxRqNexusAddressModel> NexusAddresses { get; set; }

        public List<GetOrderTaxRqLineItemModel> LineItems { get; set; }
    }

    public class GetOrderTaxRqNexusAddressModel
    {
        public string Id { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Street { get; set; }
    }

    public class GetOrderTaxRqLineItemModel
    {
        public string Id { get; set; }
        
        public int Quantity { get; set; }
        
        public string ProductTaxCode { get; set; }
        
        public float Price { get; set; }
        
        public float Discount { get; set; }
    }
}
