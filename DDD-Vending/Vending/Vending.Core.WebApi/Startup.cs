using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LightInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vending.Core.Composition;
using Vending.Core.WebApi.Mapping;

namespace Vending.Core.WebApi
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
            services.AddControllers();
            services.AddMvc()
                .AddControllersAsServices();
                //.AddJsonOptions(options =>
                //{
                //    options.JsonSerializerOptions.SerializerSettings.ContractResolver
                //    = new CamelCasePropertyNamesContractResolver();

            //})
            //.AddNewtonsoftJson(options =>
            //    {
            //        options.SerializerSettings.Converters = new List<JsonConverter>();
            //        //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //    }
            //);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(IServiceContainer container)
        {
            container.RegisterAssembly(typeof(CompositionRoot).Assembly);
            container.RegisterAssembly(GetType().Assembly);            
        }        
    }
}
