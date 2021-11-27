using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using TaxMicroserviceTakeHomeAssesment.Models.DTO.ITaxService;
using TaxMicroserviceTakeHomeAssesment.Models.DTO.TaxJar;
using TaxMicroserviceTakeHomeAssesment.Extensions;

namespace TaxMicroserviceTakeHomeAssesment.Services
{
    public class TaxJarService : ITaxService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly HttpClient _taxJarHttpClient;
        private const string _getTaxRateBaseUrl = "https://api.taxjar.com/v2/rates/{0}";
        private const string _getOrderTaxUrl = "https://api.taxjar.com/v2/taxes";

        public TaxJarService(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
            _taxJarHttpClient = BuildTaxJarHttpClient();
        }

        public TaxJarService(IConfiguration config, IMapper mapper, HttpClient taxJarHttpClient)
        {
            _config = config;
            _mapper = mapper;
            _taxJarHttpClient = taxJarHttpClient;
        }

        public async Task<GetTaxRateRsModel> GetTaxRateAsync(GetTaxRateRqModel request)
        {
            var queryString = HttpUtility.ParseQueryString(String.Empty);
            queryString.AddIfNotNull("country", request.Country);
            queryString.AddIfNotNull("state", request.State);
            queryString.AddIfNotNull("city", request.City);
            queryString.AddIfNotNull("street", request.Street);

            var url = String.Format(_getTaxRateBaseUrl, request.Zip) + (queryString.Count > 0 ? "?" + queryString.ToString() : String.Empty);
            var response = await _taxJarHttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var taxJarRs = JsonConvert.DeserializeObject<TaxJarGetTaxRateRsModel>(await response.Content.ReadAsStringAsync());
            return _mapper.Map<TaxJarGetTaxRateRsModel, GetTaxRateRsModel>(taxJarRs);
        }

        public async Task<GetOrderTaxRsModel> GetOrderTaxAsync(GetOrderTaxRqModel request)
        {
            var taxJarRq = _mapper.Map<GetOrderTaxRqModel, TaxJarGetOrderTaxRqModel>(request);
            var jsonRq = JsonConvert.SerializeObject(taxJarRq);
            var rqContent = new StringContent(jsonRq, System.Text.Encoding.UTF8, "application/json");
            var response = await _taxJarHttpClient.PostAsync(_getOrderTaxUrl, rqContent);
            response.EnsureSuccessStatusCode();
            var taxJarRs = JsonConvert.DeserializeObject<TaxJarGetOrderTaxRsModel>(await response.Content.ReadAsStringAsync());
            return _mapper.Map<TaxJarGetOrderTaxRsModel, GetOrderTaxRsModel>(taxJarRs);
        }

        protected HttpClient BuildTaxJarHttpClient()
        {
            var apiKey = _config["TaxJarApiKey"];
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            return client;
        }
    }
}
