using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using ObmultichoiceRetailer.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.Abstractions;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.Helpers;
using ObmultichoiceRetailer.Web.Services;

namespace ObmultichoiceRetailer.Web
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
            services.AddDbContext<ObmultichoiceContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ObmultichoiceContext>();

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddMediatR(typeof(Startup).Assembly);

            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddTransient<PaginatedMetaData>();
            services.AddScoped<IRepository, GenericBaseRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();

            services.AddTransient<IUriGenerator, UriGenerator>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ObmultichoiceContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            //handles Object cycle detected error
            services.AddControllersWithViews(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson(options =>
            {
                options.UseCamelCasing(true);
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).AddXmlDataContractSerializerFormatters();
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
