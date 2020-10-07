using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RektaRetailApp.Backend.Data;
using RektaRetailApp.Backend.GraphQL;
using RektaRetailApp.Backend.GraphQL.QueryTypes;

namespace RektaRetailApp.Backend
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Config = config;
        }

        public IConfiguration Config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<RektaContext>((serviceProvider, options) =>
            {
                options.UseNpgsql(Config.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
            });

            services.AddHttpContextAccessor();
            //services.AddDataLoaderRegistry();
            services.AddGraphQL(SchemaBuilder.New()
                .AddQueryType<Query>()
                .AddType<ProductQueryType>()
                .AddType<InventoryQueryType>()
                .AddType<SupplierQueryType>()
                .AddType<SuppliersInventoriesQueryType>()
                .AddType<CategoryQueryType>()
                .Create(),
                new QueryExecutionOptions{ ForceSerialExecution = false }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseGraphQL();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
