using System.Collections.Generic;

namespace TaxMicroserviceTakeHomeAssesment.Models.DTO.ITaxService
{
    public class GetOrderTaxRsModel
    {
        public float AmountToCollect { get; set; }

        public GetOrderTaxRsBreakdownModel Breakdown { get; set; }

        public bool FreightTaxable { get; set; }

        public bool HasNexus { get; set; }

        public GetOrderTaxRsJurisdictionsModel Jurisdictions { get; set; }

        public float OrderTotalAmount { get; set; }

        public float Rate { get; set; }

        public float ShippingAmount { get; set; }

        public string TaxSource { get; set; }

        public float TaxableAmount { get; set; }
    }

    public class GetOrderTaxRsBreakdownModel
    {
        public float CityTaxCollectable { get; set; }

        public float CityTaxRate { get; set; }

        public float CityTaxableAmount { get; set; }

        public float CombinedTaxRate { get; set; }

        public float CountyTaxCollectable { get; set; }

        public float CountyTaxRate { get; set; }

        public float CountyTaxableAmount { get; set; }

        public List<GetOrderTaxRsLineItemModel> LineItems { get; set; }

        public float SpecialDistrictTaxCollectable { get; set; }

        public float SpecialDistrictTaxableAmount { get; set; }

        public float SpecialTaxRate { get; set; }

        public float StateTaxCollectable { get; set; }

        public float StateTaxRate { get; set; }

        public float StateTaxableAmount { get; set; }

        public float TaxCollectable { get; set; }

        public float TaxableAmount { get; set; }
    }

    public class GetOrderTaxRsLineItemModel
    {
        public float CityAmount { get; set; }

        public float CityTaxRate { get; set; }

        public float CityTaxableAmount { get; set; }

        public float CombinedTaxRate { get; set; }

        public float CountyAmount { get; set; }

        public float CountyTaxRate { get; set; }

        public float CountyTaxableAmount { get; set; }

        public int Id { get; set; }

        public float SpecialDistrictAmount { get; set; }

        public float SpecialDistrictTaxableAmount { get; set; }

        public float SpecialTaxRate { get; set; }

        public float StateAmount { get; set; }

        public float StateSalesTaxRate { get; set; }

        public float StateTaxableAmount { get; set; }

        public float TaxCollectable { get; set; }

        public float TaxableAmount { get; set; }
    }

    public class GetOrderTaxRsJurisdictionsModel
    {
        public string City { get; set; }

        public string Country { get; set; }

        public string County { get; set; }

        public string State { get; set; }
    }
}