using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeCorpVinylProductCalalogueApi.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AcmeCorpVinylProductCalalogueApi
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? Configuration.GetConnectionString("DefaultConnection");

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin();
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyHeader();
                                  });
            });

            services.AddDbContext<VinylProductCatalogueContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/VinylProductCatalogue", options =>
            {
                options.UseCors(MyAllowSpecificOrigins);

                options.UseRouting();

                options.UseAuthorization();

                options.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            });
        }
    }
}
