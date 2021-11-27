using System.Threading.Tasks;

using TaxMicroserviceTakeHomeAssesment.Models.DTO.ITaxService;

namespace TaxMicroserviceTakeHomeAssesment.Services
{
    public delegate ITaxService TaxServiceResolver(TaxServiceType serviceType);

    public interface ITaxService
    {
        public Task<GetTaxRateRsModel> GetTaxRateAsync(GetTaxRateRqModel request);
        public Task<GetOrderTaxRsModel> GetOrderTaxAsync(GetOrderTaxRqModel request);
    }
}
