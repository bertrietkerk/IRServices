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
using InsuranceRight.Services.AddressService.Models;
using Newtonsoft.Json.Serialization;

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
                    // TODO: this ^
                });


            // DI for models
            services.AddSingleton<IAddressCheck, AddressCheck>();
            services.AddSingleton<IDataProvider, DataProvider>();
            
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
            //        name: "default",
            //        template: "{controller=AddressCheck}/{action=Index}/{id?}"
            //        );
            //});
            

        }
    }
}

