using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MtgPortfolio.Api.Services;
using MtgPortfolio.API.Entities;
using MtgPortfolio.API.Repositories;
using MtgPortfolio.API.Services;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using MtgPortfolio.Api.Automapper;

namespace MtgPortfolio.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAutoMapper();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddTransient<IMtgCardService, MtgCardService>();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<ICodesRepository, CodesRepository>();
            services.AddTransient<IMtgJsonImportService, MtgJsonImportService>();
            services.AddTransient<ICodesService, CodesService>();

            var connectionString = @"Server=(localdb)\ProjectsV13;Database=MtgPortfolio;Trusted_Connection=True;";
            services.AddDbContext<MtgPortfolioDbContext>(o => o.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mtg Portfolio V1");
            });
            
            app.UseMvc();
        }
    }
}
