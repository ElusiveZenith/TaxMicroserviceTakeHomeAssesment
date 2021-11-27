using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaxMicroserviceTakeHomeAssesment.Models.DTO.TaxJar
{
    public class TaxJarGetOrderTaxRqModel
    {
        [JsonProperty("from_country")]
        public string FromCountry { get; set; }

        [JsonProperty("from_zip")]
        public string FromZip { get; set; }

        [JsonProperty("from_state")]
        public string FromState { get; set; }

        [JsonProperty("from_city")]
        public string FromCity { get; set; }

        [JsonProperty("from_street")]
        public string FromStreet { get; set; }

        [JsonProperty("to_country")]
        public string ToCountry { get; set; }

        [JsonProperty("to_zip")]
        public string ToZip { get; set; }

        [JsonProperty("to_state")]
        public string ToState { get; set; }

        [JsonProperty("to_city")]
        public string ToCity { get; set; }

        [JsonProperty("to_street")]
        public string ToStreet { get; set; }

        public float Amount { get; set; }

        [JsonProperty("shipping")]
        public float ShippingAmmount { get; set; }

        // Not sure how this would be used, or if this is unique to tax jar or a common thing.
        // If it is not common, I would consider having the service get this information from another sorce and not need it to be passed with the request.
        // If I was doing this as part of a story, I would ask some more questions about how it will be used and long term plans to figure out if it should be part of the interface request.
        [JsonProperty("customer_id")] 
        public string CostomerId { get; set; }

        [JsonProperty("exemption_type")]
        public string ExemptionType { get; set; }

        [JsonProperty("nexus_addresses")]
        public List<TaxJarGetOrderTaxRqNexusAddressModel> NexusAddresses { get; set; }

        [JsonProperty("line_items")]
        public List<TaxJarGetOrderTaxRqLineItemModel> LineItems { get; set; }
    }

    public class TaxJarGetOrderTaxRqNexusAddressModel
    {
        public string Id { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Street { get; set; }
    }

    public class TaxJarGetOrderTaxRqLineItemModel
    {
        public string Id { get; set; }


        public int Quantity { get; set; }

        [JsonProperty("product_tax_code")]
        public string ProductTaxCode { get; set; }

        [JsonProperty("unit_price")]
        public float Price { get; set; }

        public float Discount { get; set; }
    }
}
