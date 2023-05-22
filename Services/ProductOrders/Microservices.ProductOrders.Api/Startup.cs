using MediatR;
using Microservices.ProductOrders.Application.CommandHandlers;
using Microservices.ProductOrders.Infrastructure;
using Microservices.SharedLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.ProductOrders.Api
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
            services.AddMediatR(typeof(CreatedOrderCommandHandler).Assembly); //ekledik

            services.AddScoped<ISharedIdentityService,SharedIdentityService>(); //ekledik

            services.AddHttpContextAccessor(); //ekledik

            services.AddDbContext<OrderDbContext>(opt => //ekledik
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),configure =>
                {
                    configure.MigrationsAssembly("Microservices.ProductOrders.Infrastructure");
                });
            });
            //Koruma altýna almak IdentityServer.Api-Cofig.cs
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = Configuration["IdentityServerURL"];
                opt.Audience = "resource_product_orders";
                opt.RequireHttpsMetadata = false;
            });

            //Koruma altýna almak
            var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
            });
            //Token Ýçinde gelen claimlerden IdentiInfier'i Sub olarak ayarlama
            //{sub: 50e711b4-955a-4611-af35-e627315eca3d}
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
     
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.ProductOrders.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.ProductOrders.Api v1"));
            }

            app.UseRouting();
            app.UseAuthentication(); //ekledik
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
