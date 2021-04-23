using Core.CQRS.API.Context;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using static Core.CQRS.API.Features.ProductFeatures.Commands.CreateProductCommand;
using static Core.CQRS.API.Features.ProductFeatures.Commands.DeleteProductByIdCommand;
using static Core.CQRS.API.Features.ProductFeatures.Commands.UpdateProductCommand;
using static Core.CQRS.API.Features.ProductFeatures.Queries.GetAllProductsQuery;
using static Core.CQRS.API.Features.ProductFeatures.Queries.GetProductByIdQuery;

namespace Core.CQRS.API
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
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IApplicationContext, ApplicationContext>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            //services.AddScoped<CreateProductCommandHandler>();
            //services.AddScoped<DeleteProductByIdCommandHandler>();
            //services.AddScoped<UpdateProductCommandHandler>();
            //services.AddScoped<GetAllProductsQueryHandler>();
            //services.AddScoped<GetProductByIdQueryHandler>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                //c.IncludeXmlComments(string.Format(@"{0}\Core.CQRS.API.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Core.CQRS.API",
                });
            });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core.CQRS.API");
            });
        }
    }
}
