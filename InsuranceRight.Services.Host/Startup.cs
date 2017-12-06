using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using InsuranceRight.Services.AddressService.Interfaces;
using InsuranceRight.Services.AddressService.Repositories;
using InsuranceRight.Services.Feature.Car.Services.Impl;
using InsuranceRight.Services.Feature.Car.Services.Data;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Feature.Car.Services.Data.Impl;

namespace InsuranceRight.Services.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // DI Address
            services.AddSingleton<IAddressCheck, AddressCheckRepository>();
            services.AddSingleton<IDataProvider, AddressDataProvider>();

            // DI Car
            services.AddSingleton<ILicensePlateLookup, DefaultLicensePlateLookup>();
            services.AddSingleton<ICarDataProvider, DefaultCarDataProvider>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();


        }
    }
}
