using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxMicroserviceTakeHomeAssesment.Mapping;

namespace TaxMicroserviceTakeHomeAssesment.Test
{
    public class TestSetup
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public TestSetup()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                     path: "appsettings.json",
                     optional: false,
                     reloadOnChange: true)
               .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            var mapConfig = new MapperConfiguration(x => x.AddProfile<TaxJarProfile>());
            serviceCollection.AddSingleton<IMapper>(mapConfig.CreateMapper());

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
