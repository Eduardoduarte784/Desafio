using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaConsultorio.Application.Service;
using AgendaConsultorio.Application.Service.Interface;
using AgendaConsultorio.Domain.Repository;
using AgendaConsultorio.Infra.Data.Context;
using AgendaConsultorio.Infra.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Design;

namespace AgendaConsultorio.Api
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
            services.AddScoped<IConsultaService, ConsultaService>();
            services.AddScoped<IConsultaRepository, ConsultaRepository>();

            services.AddDbContextPool<AgendaConsultorioContext>(this.Builder());

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", buider =>
                {
                    buider
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .Build();
                });
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("EnableCORS");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public Action<DbContextOptionsBuilder> Builder()
        {
            Action<SqlServerDbContextOptionsBuilder> sqlOptions = null;
            var migrationsAssemblyName = "AgendaConsultorio.Infra.Data.Migrations";
            if (!string.IsNullOrEmpty(migrationsAssemblyName))
                sqlOptions = (options) => options.MigrationsAssembly(migrationsAssemblyName);

            return options => options.UseSqlServer(this.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value, sqlOptions);
        }
    }
}
