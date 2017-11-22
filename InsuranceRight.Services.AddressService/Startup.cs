﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using InsuranceRight.Services.AddressService.Interfaces;
using InsuranceRight.Services.AddressService.Repositories;

namespace InsuranceRight.Services.AddressService
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
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    var settings = options.SerializerSettings;
                    settings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });


            // DI for models
            services.AddSingleton<IAddressCheck, AddressCheckRepository>();
            services.AddSingleton<IDataProvider, DataProviderRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "ValidateZipcode",
            //        template: "{controller}/{action}/{zipcode}",
            //        defaults: new { controller=}
            //        );
            //});


        }
    }
}

