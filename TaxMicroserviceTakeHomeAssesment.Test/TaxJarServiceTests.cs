using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Xunit;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TaxMicroserviceTakeHomeAssesment.Models.DTO.ITaxService;
using TaxMicroserviceTakeHomeAssesment.Models.DTO.TaxJar;
using TaxMicroserviceTakeHomeAssesment.Services;
using System;
using System.Collections.Generic;
using System.Collections;

namespace TaxMicroserviceTakeHomeAssesment.Test
{
    public class TaxJarServiceTests : IClassFixture<TestSetup>
    {
        private ServiceProvider _serviceProvider;
        private IConfiguration _config;
        private IMapper _mapper;

        public TaxJarServiceTests(TestSetup testSetup)
        {
            _serviceProvider = testSetup.ServiceProvider;
            _config = _serviceProvider.GetService<IConfiguration>();
            _mapper = _serviceProvider.GetService<IMapper>();
        }

        [Theory]
        [InlineData("US", "12345", "NY", "New York", "123 Some Street", "https://api.taxjar.com/v2/rates/12345?country=US&state=NY&city=New+York&street=123+Some+Street")]
        [InlineData(null, "12345", null, null, null, "https://api.taxjar.com/v2/rates/12345")]
        [InlineData(null, "12345", "NY", null, null, "https://api.taxjar.com/v2/rates/12345?state=NY")]
        public void GetTaxRateAsync_SimpleMockedRequest(string country, string zip, string state, string city, string street, string expectedRequestUrlString)
        {
            // Arrange
            var serviceRequest = new GetTaxRateRqModel
            {
                Country = country,
                Zip = zip,
                State = state,
                City = city,
                Street = street
            };
            var httpResponce = new TaxJarGetTaxRateRsModel
            {
                Rate = new TaxJarGetTaxRateRsRateModel
                {
                    City = city,
                    CityRate = 1,
                    CombinedDistrictRate = 2,
                    CombinedRate = 3,
                    Country = country,
                    CountryRate = 4,
                    County = "Some County",
                    CountyRate = 5,
                    FreightTaxable = true,
                    State = state,
                    StateRate = 6,
                    Zip = zip
                }
            };
            var expectedServiceResponce = new GetTaxRateRsModel
            {
                City = city,
                CityRate = 1,
                CombinedDistrictRate = 2,
                CombinedRate = 3,
                Country = country,
                CountryRate = 4,
                County = "Some County",
                CountyRate = 5,
                FreightTaxable = true,
                State = state,
                StateRate = 6,
                Zip = zip
            };
            HttpRequestMessage httpRequest = null;

            // Act
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    httpRequest = request;
                    HttpResponseMessage response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent(JsonConvert.SerializeObject(httpResponce));
                    return response;
                });

            var service = new TaxJarService(_config, _mapper, new HttpClient(mockHttpMessageHandler.Object));
            var serviceResponce = service.GetTaxRateAsync(serviceRequest).Result;

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expectedServiceResponce), JsonConvert.SerializeObject(serviceResponce));
            Assert.Equal(expectedRequestUrlString, httpRequest?.RequestUri.ToString());
        }

        [Theory]
        [ClassData(typeof(GetOrderTaxAsync_SimpleMockedRequest_TestData))]
        public void GetOrderTaxAsync_SimpleMockedRequest(GetOrderTaxRqModel request)
        {
            // Arrange
            var expectedRequestBody = new TaxJarGetOrderTaxRqModel
            {
                FromCountry = request.FromCountry,
                FromZip = request.FromZip,
                FromState = request.FromState,
                FromCity = request.FromCity,
                FromStreet = request.FromStreet,
                ToCountry = request.ToCountry,
                ToZip = request.ToZip,
                ToState = request.ToState,
                ToCity = request.ToCity,
                ToStreet = request.ToStreet,
                Amount = request.Amount,
                ShippingAmmount = request.ShippingAmmount,
                ExemptionType = request.ExemptionType,
                NexusAddresses = new List<TaxJarGetOrderTaxRqNexusAddressModel>
                {
                    new TaxJarGetOrderTaxRqNexusAddressModel
                    {
                        Id = request.NexusAddresses[0].Id,
                        Country = request.NexusAddresses[0].Country,
                        Zip = request.NexusAddresses[0].Zip,
                        State = request.NexusAddresses[0].State,
                        City = request.NexusAddresses[0].City,
                        Street = request.NexusAddresses[0].Street
                    }
                },
                LineItems = new List<TaxJarGetOrderTaxRqLineItemModel>
                {
                    new TaxJarGetOrderTaxRqLineItemModel
                    {
                        Id = request.LineItems[0].Id,
                        Quantity = request.LineItems[0].Quantity,
                        ProductTaxCode = request.LineItems[0].ProductTaxCode,
                        Price = request.LineItems[0].Price,
                        Discount = request.LineItems[0].Discount
                    }
                },
            };
            var httpResponce = new TaxJarGetOrderTaxRsModel
            {
                Tax = new TaxJarGetOrderTaxRsTaxModel
                {
                    AmountToCollect = 0,
                    Breakdown = new TaxJarGetOrderTaxRsBreakdownModel
                    {
                        CityTaxCollectable = 1,
                        CityTaxRate = 2,
                        CityTaxableAmount = 3,
                        CombinedTaxRate = 4,
                        CountyTaxCollectable = 5,
                        CountyTaxRate = 6,
                        CountyTaxableAmount = 7,
                        LineItems = new List<TaxJarGetOrderTaxRsLineItemModel>
                        {
                            new TaxJarGetOrderTaxRsLineItemModel
                            {
                                CityAmount = 20,
                                CityTaxRate = 21,
                                CityTaxableAmount = 22,
                                CombinedTaxRate = 23,
                                CountyAmount = 24,
                                CountyTaxRate = 25,
                                CountyTaxableAmount = 26,
                                Id = 27,
                                SpecialDistrictAmount = 28,
                                SpecialDistrictTaxableAmount = 29,
                                SpecialTaxRate = 30,
                                StateAmount = 31,
                                StateSalesTaxRate = 32,
                                StateTaxableAmount = 33,
                                TaxCollectable = 34,
                                TaxableAmount = 35
                            }
                        },
                        SpecialDistrictTaxCollectable = 8,
                        SpecialDistrictTaxableAmount = 9,
                        SpecialTaxRate = 10,
                        StateTaxCollectable = 11,
                        StateTaxRate = 12,
                        StateTaxableAmount = 13,
                        TaxCollectable = 14,
                        TaxableAmount = 15
                    },
                    FreightTaxable = false,
                    HasNexus = true,
                    Jurisdictions = new TaxJarGetOrderTaxRsJurisdictionsModel
                    {
                        City = "",
                        Country = "",
                        County = "",
                        State = ""
                    },
                    OrderTotalAmount = 16,
                    Rate = 17,
                    ShippingAmount = 18,
                    TaxSource = "",
                    TaxableAmount = 19
                }
            };
            var expectedServiceResponce = new GetOrderTaxRsModel
            {
                    AmountToCollect = httpResponce.Tax.AmountToCollect,
                    Breakdown = new GetOrderTaxRsBreakdownModel
                    {
                        CityTaxCollectable = httpResponce.Tax.Breakdown.CityTaxCollectable,
                        CityTaxRate = httpResponce.Tax.Breakdown.CityTaxRate,
                        CityTaxableAmount = httpResponce.Tax.Breakdown.CityTaxableAmount,
                        CombinedTaxRate = httpResponce.Tax.Breakdown.CombinedTaxRate,
                        CountyTaxCollectable = httpResponce.Tax.Breakdown.CountyTaxCollectable,
                        CountyTaxRate = httpResponce.Tax.Breakdown.CountyTaxRate,
                        CountyTaxableAmount = httpResponce.Tax.Breakdown.CountyTaxableAmount,
                        LineItems = new List<GetOrderTaxRsLineItemModel>
                        {
                            new GetOrderTaxRsLineItemModel
                            {
                                CityAmount = httpResponce.Tax.Breakdown.LineItems[0].CityAmount,
                                CityTaxRate = httpResponce.Tax.Breakdown.LineItems[0].CityTaxRate,
                                CityTaxableAmount = httpResponce.Tax.Breakdown.LineItems[0].CityTaxableAmount,
                                CombinedTaxRate = httpResponce.Tax.Breakdown.LineItems[0].CombinedTaxRate,
                                CountyAmount = httpResponce.Tax.Breakdown.LineItems[0].CountyAmount,
                                CountyTaxRate = httpResponce.Tax.Breakdown.LineItems[0].CountyTaxRate,
                                CountyTaxableAmount = httpResponce.Tax.Breakdown.LineItems[0].CountyTaxableAmount,
                                Id = httpResponce.Tax.Breakdown.LineItems[0].Id,
                                SpecialDistrictAmount = httpResponce.Tax.Breakdown.LineItems[0].SpecialDistrictAmount,
                                SpecialDistrictTaxableAmount = httpResponce.Tax.Breakdown.LineItems[0].SpecialDistrictTaxableAmount,
                                SpecialTaxRate = httpResponce.Tax.Breakdown.LineItems[0].SpecialTaxRate,
                                StateAmount = httpResponce.Tax.Breakdown.LineItems[0].StateAmount,
                                StateSalesTaxRate = httpResponce.Tax.Breakdown.LineItems[0].StateSalesTaxRate,
                                StateTaxableAmount = httpResponce.Tax.Breakdown.LineItems[0].StateTaxableAmount,
                                TaxCollectable = httpResponce.Tax.Breakdown.LineItems[0].TaxCollectable,
                                TaxableAmount = httpResponce.Tax.Breakdown.LineItems[0].TaxableAmount
                            }
                        },
                        SpecialDistrictTaxCollectable = httpResponce.Tax.Breakdown.SpecialDistrictTaxCollectable,
                        SpecialDistrictTaxableAmount = httpResponce.Tax.Breakdown.SpecialDistrictTaxableAmount,
                        SpecialTaxRate = httpResponce.Tax.Breakdown.SpecialTaxRate,
                        StateTaxCollectable = httpResponce.Tax.Breakdown.StateTaxCollectable,
                        StateTaxRate = httpResponce.Tax.Breakdown.StateTaxRate,
                        StateTaxableAmount = httpResponce.Tax.Breakdown.StateTaxableAmount,
                        TaxCollectable = httpResponce.Tax.Breakdown.TaxCollectable,
                        TaxableAmount = httpResponce.Tax.Breakdown.TaxableAmount
                    },
                    FreightTaxable = httpResponce.Tax.FreightTaxable,
                    HasNexus = httpResponce.Tax.HasNexus,
                    Jurisdictions = new GetOrderTaxRsJurisdictionsModel
                    {
                        City = httpResponce.Tax.Jurisdictions.City,
                        Country = httpResponce.Tax.Jurisdictions.Country,
                        County = httpResponce.Tax.Jurisdictions.County,
                        State = httpResponce.Tax.Jurisdictions.State
                    },
                    OrderTotalAmount = httpResponce.Tax.OrderTotalAmount,
                    Rate = httpResponce.Tax.Rate,
                    ShippingAmount = httpResponce.Tax.ShippingAmount,
                    TaxSource = httpResponce.Tax.TaxSource,
                    TaxableAmount = httpResponce.Tax.TaxableAmount
            };
            HttpRequestMessage httpRequest = null;
            
            // Act
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    httpRequest = request;
                    HttpResponseMessage response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent(JsonConvert.SerializeObject(httpResponce));
                    return response;
                });

            var service = new TaxJarService(_config, _mapper, new HttpClient(mockHttpMessageHandler.Object));
            var serviceResponce = service.GetOrderTaxAsync(request).Result;

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expectedServiceResponce), JsonConvert.SerializeObject(serviceResponce));
            Assert.Equal(JsonConvert.SerializeObject(expectedRequestBody), httpRequest.Content.ReadAsStringAsync().Result);
        }

        public class GetOrderTaxAsync_SimpleMockedRequest_TestData : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] 
                { 
                    new GetOrderTaxRqModel 
                    {
                        FromCountry = "",
                        FromZip = "",
                        FromState = "",
                        FromCity = "",
                        FromStreet = "",
                        ToCountry = "",
                        ToZip = "",
                        ToState = "",
                        ToCity = "",
                        ToStreet = "",
                        Amount = 1,
                        ShippingAmmount = 2,
                        ExemptionType = "",
                        NexusAddresses = new List<GetOrderTaxRqNexusAddressModel>
                        {
                            new GetOrderTaxRqNexusAddressModel
                            {
                                Id = "",
                                Country = "",
                                Zip = "",
                                State = "",
                                City = "",
                                Street = ""
                            }
                        },
                        LineItems = new List<GetOrderTaxRqLineItemModel>
                        {
                            new GetOrderTaxRqLineItemModel
                            {
                                Id = "",
                                Quantity = 1,
                                ProductTaxCode = "",
                                Price = 2,
                                Discount = 3
                            }
                        },
                    }
                },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
