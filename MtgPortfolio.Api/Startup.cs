using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MtgPortfolio.API.Automapper;
using MtgPortfolio.API.Entities;
using MtgPortfolio.API.Entities.Codes;
using MtgPortfolio.API.Models;
using MtgPortfolio.API.Repositories;
using MtgPortfolio.API.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace MtgPortfolio.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddTransient<IMtgCardService, MtgCardService>();
            services.AddTransient<IMtgPortfolioRepository, MtgPortfolioRepository>();
            services.AddTransient<IMtgPortfolioCodesRepository, MtgPortfolioCodesRepository>();
            services.AddTransient<IMtgJsonImportService, MtgJsonImportService>();
            
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

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<MtgCardEntity, MtgCard>()
                    .ReverseMap();

                config.CreateMap<string, LayoutEntity>()
                    .ApplyBaseMapping();
                config.CreateMap<string, BorderEntity>()
                    .ApplyBaseMapping();
                config.CreateMap<string, ColorEntity>()
                    .ApplyBaseMapping();
                config.CreateMap<string, FormatEntity>()
                    .ApplyBaseMapping();
                config.CreateMap<string, LegalityEntity>()
                    .ApplyBaseMapping();
                config.CreateMap<string, RarityEntity>()
                    .ApplyBaseMapping();
                config.CreateMap<string, SetEntity>()
                    .ApplyBaseMapping();
                config.CreateMap<string, SubtypeEntity>()
                    .ApplyBaseMapping();
                config.CreateMap<string, SupertypeEntity>()
                    .ApplyBaseMapping();
                config.CreateMap<string, TypeEntity>()
                    .ApplyBaseMapping();
            });

            app.UseMvc();
        }
    }
}
