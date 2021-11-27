using Newtonsoft.Json;

namespace TaxMicroserviceTakeHomeAssesment.Models.DTO.TaxJar
{
    public class TaxJarGetTaxRateRsModel
    {
        public TaxJarGetTaxRateRsRateModel Rate { get; set; }
    }

    public class TaxJarGetTaxRateRsRateModel
    {
        public string City { get; set; }

        [JsonProperty("city_rate")]
        public float CityRate { get; set; }

        [JsonProperty("combined_district_rate")]
        public float CombinedDistrictRate { get; set; }

        [JsonProperty("combined_rate")]
        public float CombinedRate { get; set; }

        public string Country { get; set; }

        [JsonProperty("country_rate")]
        public float CountryRate { get; set; }

        public string County { get; set; }

        [JsonProperty("county_rate")]
        public float CountyRate { get; set; }

        [JsonProperty("freight_taxable")]
        public bool FreightTaxable { get; set; }

        public string State { get; set; }

        [JsonProperty("state_rate")]
        public float StateRate { get; set; }

        public string Zip { get; set; }
    }
}
