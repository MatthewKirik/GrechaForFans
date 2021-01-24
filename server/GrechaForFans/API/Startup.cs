using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapperConfiguration;
using BLL.Services;
using BLL.Services.Implementations;
using DAL.Repositories;
using DAL.Repositories.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;

namespace API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            MigrateDatabase();

            services.AddDbContext<DAL.BuckwheatContext>();

            services.AddTransient<ILotsRepository, LotsRepository>();
            services.AddTransient<IPricesRepository, PricesRepository>();
            services.AddTransient<IShopsRepository, ShopsRepository>();

            services.AddTransient<ILotsService, LotsService>();
            services.AddTransient<IPricesService, PricesService>();
            services.AddTransient<IShopsService, ShopsService>();

            services.AddHostedService<ParsingService>();

            services.AddControllers();

            services.AddSingleton(AutoMapperConfigurator.GetMapper());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "API Docs", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1 documentation");
                c.RoutePrefix = string.Empty;
            });
        }

        private void MigrateDatabase()
        {
            using (var db = new DAL.BuckwheatContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
