using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Feature.Car.Services.Impl;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using InsuranceRight.Services.Feature.Car.Services.Data;
using InsuranceRight.Services.Feature.Car.Services.Data.Impl;

namespace InsuranceRight.Services.Feature.Car
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

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new Info
                {
                    Title = "InsuranceRight Services",
                    Version = "1.0.0.0",
                    Description = "Documentation on the services for InsuranceRight",
                    Contact = new Contact { Name = "Virtual Affairs", Email = "", Url = "http://wwww.virtual-affairs.com/" },
                    License = new License { Name = "License", Url = "https://en.wikipedia.org/wiki/License" }
                });

                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //var xmlPath = Path.Combine(basePath, "xml file!!!");
                //c.IncludeXmlComments(xmlPath);
            });

            // DI
            //services.AddSingleton<ILicensePlateLookup, DefaultLicensePlateLookup>();
            //services.AddSingleton<ICarDataProvider, DefaultCarDataProvider>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
