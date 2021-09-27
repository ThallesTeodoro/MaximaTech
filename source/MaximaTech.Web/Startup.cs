using System;
using AutoMapper;
using MaximaTech.Core.Entities;
using MaximaTech.Domain.Commands.Responses;
using MaximaTech.Web.Configuration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace MaximaTech.Web
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
            services.AddDbContextSettings(Configuration);

            services.AddApplicationServices();

            services.AddMediatR(AppDomain.CurrentDomain.Load("MaximaTech.Domain"));

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            services.AddHttpContextAccessor();

            services.AddAuthentication("MaximaTechScheme")
                .AddCookie("MaximaTechScheme", options =>
                {
                    options.AccessDeniedPath = "/login";
                    options.LoginPath = "/login";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.Cookie.Name = "MaximaTechScheme.Auth";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "Api",
                    pattern: "{area=Api}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
