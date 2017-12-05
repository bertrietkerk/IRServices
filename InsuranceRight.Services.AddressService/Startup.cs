using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using InsuranceRight.Services.AddressService.Interfaces;
using InsuranceRight.Services.AddressService.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

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

            services.AddSwaggerGen(c =>
            {
                
                c.SwaggerDoc("v1", new Info
                {
                    Title = "InsuranceRight Services",
                    Version = "1.0.0.0",
                    Description = "Documentation on the services for InsuranceRight",
                    Contact = new Contact { Name = "Virtual Affairs", Email = "", Url = "http://wwww.virtual-affairs.com/"},
                    License = new License { Name = "License", Url = "https://en.wikipedia.org/wiki/License" }
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "InsuranceRight.Services.AddressService.xml");
                c.IncludeXmlComments(xmlPath);
            });


            // DI for models
            services.AddSingleton<IAddressCheck, AddressCheckRepository>();
            services.AddSingleton<IDataProvider, AddressDataProvider>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui, specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });



        }
    }
}

