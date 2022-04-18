using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TM.TestService.Infrastructure.Entities;
using TM.TestService.Infrastructure.Extensions;

namespace TestMaker.Api
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
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TM.TestService.Api", Version = "v1" });
            });

            if (!string.IsNullOrEmpty(Configuration["Mssql:ConnectionString"]))
            {
                services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
                {
                    optionsBuilder.UseSqlServer(Configuration["Mssql:ConnectionString"]);
                });
            }

            if (!string.IsNullOrEmpty(Configuration["Sqlite:ConnectionString"]))
            {
                services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
                {
                    optionsBuilder.UseSqlite(Configuration["Sqlite:ConnectionString"]);
                });
            }

            services.AddTransient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment() || env.EnvironmentName == "Production")
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TM.TestMaker.Api v1"));
            }

            app.UseCors("AllowAll");

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
