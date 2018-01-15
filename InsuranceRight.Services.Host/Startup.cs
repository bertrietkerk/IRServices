using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InsuranceRight.Services.AddressService.Interfaces;
using InsuranceRight.Services.AddressService.Repositories;
using InsuranceRight.Services.Feature.Car.Services.Impl;
using InsuranceRight.Services.Feature.Car.Services.Data;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Feature.Car.Services.Data.Impl;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using InsuranceRight.Services.Acceptance.Services;
using InsuranceRight.Services.Acceptance.Services.Impl;
using InsuranceRight.Services.Models.Settings;
using System.IO;

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
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    var settings = options.SerializerSettings;
                    settings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                }); ;

            var xmlDocs = Directory.GetFiles(@"C:\Projects\InsuranceRight.Services\InsuranceRight.Services.Host\XmlCommentDocs");

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info 
                    {
                        Title = "InsuranceRight Services",
                        Version = "1.0.0.0",
                        Description = "Documentation on the services for InsuranceRight",
                        Contact = new Contact { Name = "Virtual Affairs", Email = "", Url = "http://wwww.virtual-affairs.com/" },
                        License = new License { Name = "License", Url = "https://en.wikipedia.org/wiki/License" }
                    });

                foreach (var doc in xmlDocs)
                {
                    c.IncludeXmlComments(doc);
                }
            });

            // DI appsettings.json
            services.AddOptions();
            //services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<AcceptanceSettings>(Configuration.GetSection("AcceptanceSettings"));
            services.Configure<PremiumCalculationSettings>(Configuration.GetSection("PremiumCalculationSettings"));
            services.Configure<DiscountSettings>((settings) =>
            {
                Configuration.GetSection("DiscountSettings").Bind(settings);
            });

            // DI Address
            services.AddSingleton<IAddressLookup, DefaultAddressLookup>();
            services.AddSingleton<IAddressDataProvider, DefaultAddressDataProvider>();

            // DI Car
            services.AddSingleton<ILicensePlateLookup, DefaultLicensePlateLookup>();
            services.AddSingleton<ICarDataProvider, DefaultCarDataProvider>();
            services.AddSingleton<ICarLookup, DefaultCarLookup>();
            services.AddSingleton<ICarDiscountPolicy, DefaultCarDiscountPolicy>();
            services.AddSingleton<ICarPremiumPolicy, DefaultCarPremiumPolicy>();
            services.AddSingleton<IPremiumCalculator, DefaultPremiumCalculator>();
            services.AddSingleton<ICarDocumentService, DefaultCarDocumentService>();

            // DI Acceptance
            services.AddSingleton<IAcceptanceCheck, DefaultAcceptanceCheck>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Host of Services - Documentation v1");
            });
        }
    }
}
