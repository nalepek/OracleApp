using System.IO;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OracleApp.Application.Product;
using OracleApp.Infrastructure.Persistence;
using OracleApp.Infrastructure.Persistence.QueryBuilders.Product;
using OracleApp.Infrastructure.Persistence.Repositories;
using OracleApp.Infrastructure.Persistence.Repositories.Product;
using OracleApp.Infrastructure.Persistence.Searchers.Product;
using OracleApp.Middleware;

namespace OracleApp
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
            services.AddTransient<IProductQueryService, ProductQueryService>();
            services.AddTransient<IProductSearcher, ProductQueryBuilder>();
            services.AddTransient<IProductCommandService, ProductCommandService>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddMediatR(typeof(Startup));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
                });
            });

            services.AddMvc();

            OracleContext.ConnectionString = $"User Id=system;Password=system;Data Source=127.0.0.1:1521/xe";
            OracleContextAsync.ConnectionString = $"User Id=system;Password=system;Data Source=127.0.0.1:1521/xe";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandlingMiddleware();

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "./App/src/index.html";
                    await next();
                }
            });

            app.UseCors("AllowAll");
            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
