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

namespace API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DAL.BuckwheatContext>();

            services.AddScoped<ILotsRepository, LotsRepository>();
            services.AddScoped<IPricesRepository, PricesRepository>();
            services.AddScoped<IShopsRepository, ShopsRepository>();

            services.AddScoped<ILotsService, LotsService>();
            services.AddScoped<IPricesService, PricesService>();
            services.AddScoped<IShopsService, ShopsService>();


            services.AddControllers();

            services.AddSingleton(AutoMapperConfigurator.GetMapper());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            MigrateDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
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
