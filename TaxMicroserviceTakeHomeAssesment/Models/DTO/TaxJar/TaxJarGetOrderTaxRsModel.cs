using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaxMicroserviceTakeHomeAssesment.Models.DTO.TaxJar
{
    public class TaxJarGetOrderTaxRsModel
    {
        public TaxJarGetOrderTaxRsTaxModel Tax { get; set; }
    }

    
    public class TaxJarGetOrderTaxRsTaxModel
    {
        [JsonProperty("amount_to_collect")]
        public float AmountToCollect { get; set; }

        public TaxJarGetOrderTaxRsBreakdownModel Breakdown { get; set; }

        [JsonProperty("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonProperty("has_nexus")]
        public bool HasNexus { get; set; }

        public TaxJarGetOrderTaxRsJurisdictionsModel Jurisdictions { get; set; }

        [JsonProperty("order_total_amount")]
        public float OrderTotalAmount { get; set; }

        public float Rate { get; set; }

        [JsonProperty("shipping")]
        public float ShippingAmount { get; set; }

        [JsonProperty("tax_source")]
        public string TaxSource { get; set; }

        [JsonProperty("taxable_amount")]
        public float TaxableAmount { get; set; }
    }

    public class TaxJarGetOrderTaxRsBreakdownModel
    {
        [JsonProperty("city_tax_collectable")]
        public float CityTaxCollectable { get; set; }

        [JsonProperty("city_tax_rate")]
        public float CityTaxRate { get; set; }

        [JsonProperty("city_taxable_amount")]
        public float CityTaxableAmount { get; set; }

        [JsonProperty("combined_tax_rate")]
        public float CombinedTaxRate { get; set; }

        [JsonProperty("county_tax_collectable")]
        public float CountyTaxCollectable { get; set; }

        [JsonProperty("county_tax_rate")]
        public float CountyTaxRate { get; set; }

        [JsonProperty("county_taxable_amount")]
        public float CountyTaxableAmount { get; set; }

        [JsonProperty("line_items")]
        public List<TaxJarGetOrderTaxRsLineItemModel> LineItems { get; set; }

        [JsonProperty("special_district_tax_collectable")]
        public float SpecialDistrictTaxCollectable { get; set; }

        [JsonProperty("special_district_taxable_amount")]
        public float SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("special_tax_rate")]
        public float SpecialTaxRate { get; set; }

        [JsonProperty("state_tax_collectable")]
        public float StateTaxCollectable { get; set; }

        [JsonProperty("state_tax_rate")]
        public float StateTaxRate { get; set; }

        [JsonProperty("state_taxable_amount")]
        public float StateTaxableAmount { get; set; }

        [JsonProperty("tax_collectable")]
        public float TaxCollectable { get; set; }

        [JsonProperty("taxable_amount")]
        public float TaxableAmount { get; set; }
    }

    public class TaxJarGetOrderTaxRsLineItemModel
    {
        [JsonProperty("city_amount")]
        public float CityAmount { get; set; }

        [JsonProperty("city_tax_rate")]
        public float CityTaxRate { get; set; }

        [JsonProperty("city_taxable_amount")]
        public float CityTaxableAmount { get; set; }

        [JsonProperty("combined_tax_rate")]
        public float CombinedTaxRate { get; set; }

        [JsonProperty("county_amount")]
        public float CountyAmount { get; set; }

        [JsonProperty("county_tax_rate")]
        public float CountyTaxRate { get; set; }

        [JsonProperty("county_taxable_amount")]
        public float CountyTaxableAmount { get; set; }

        public int Id { get; set; }

        [JsonProperty("special_district_amount")]
        public float SpecialDistrictAmount { get; set; }

        [JsonProperty("special_district_taxable_amount")]
        public float SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("special_tax_rate")]
        public float SpecialTaxRate { get; set; }

        [JsonProperty("state_amount")]
        public float StateAmount { get; set; }

        [JsonProperty("state_sales_tax_rate")]
        public float StateSalesTaxRate { get; set; }

        [JsonProperty("state_taxable_amount")]
        public float StateTaxableAmount { get; set; }

        [JsonProperty("tax_collectable")]
        public float TaxCollectable { get; set; }

        [JsonProperty("taxable_amount")]
        public float TaxableAmount { get; set; }
    }

    public class TaxJarGetOrderTaxRsJurisdictionsModel
    {
        public string City { get; set; }

        public string Country { get; set; }

        public string County { get; set; }

        public string State { get; set; }
    }
}