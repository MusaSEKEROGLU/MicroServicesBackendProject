using Microservices.Beverages.Api.Services.Abstract;
using Microservices.Beverages.Api.Services.Concrete;
using Microservices.Beverages.Api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Microservices.Beverages.Api
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = Configuration["IdentityServerURL"];
                opt.Audience = "resource_beverages";
                opt.RequireHttpsMetadata = false;
            });

            services.AddScoped<ICategoryService, CategoryService>(); //ekledik

            services.AddScoped<IBeveragesService, BeveragesService>(); // ekledik

            services.AddAutoMapper(typeof(Startup)); //ekledik
            

            //Koruma altýna almak Authorize
            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter());

            });

            //ekledik 2ad
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddSingleton<IDatabaseSettings>(serprovider =>
            {
                return serprovider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Beverages.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Beverages.Api v1"));
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
