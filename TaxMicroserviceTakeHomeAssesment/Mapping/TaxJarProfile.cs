using AutoMapper;

using TaxMicroserviceTakeHomeAssesment.Models.DTO.ITaxService;
using TaxMicroserviceTakeHomeAssesment.Models.DTO.TaxJar;

namespace TaxMicroserviceTakeHomeAssesment.Mapping
{
    public class TaxJarProfile : Profile
    {
        public TaxJarProfile()
        {
            // GetTaxRate
            CreateMap<TaxJarGetTaxRateRsRateModel, GetTaxRateRsModel>();
            CreateMap<TaxJarGetTaxRateRsModel, GetTaxRateRsModel>()
                .IncludeMembers(dest => dest, x => x.Rate);

            // GetOrderTax
            CreateMap<GetOrderTaxRqNexusAddressModel, TaxJarGetOrderTaxRqNexusAddressModel>();
            CreateMap<GetOrderTaxRqLineItemModel, TaxJarGetOrderTaxRqLineItemModel>();
            CreateMap<GetOrderTaxRqModel, TaxJarGetOrderTaxRqModel>();
            CreateMap<TaxJarGetOrderTaxRsTaxModel, GetOrderTaxRsModel>();
            CreateMap<TaxJarGetOrderTaxRsModel, GetOrderTaxRsModel>()
                .IncludeMembers(dest => dest, x => x.Tax);
            CreateMap<TaxJarGetOrderTaxRsBreakdownModel, GetOrderTaxRsBreakdownModel>();
            CreateMap<TaxJarGetOrderTaxRsLineItemModel, GetOrderTaxRsLineItemModel>();
            CreateMap<TaxJarGetOrderTaxRsJurisdictionsModel, GetOrderTaxRsJurisdictionsModel>();
        }
    }
}
