using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Application.Instance = new Application();
            Application.Instance.Configuration = Configuration;
            Application.Instance.Load();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
            Application.Instance.Redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { { Configuration.GetSection("Redis")["Endpoint"] } },
                Password = Configuration.GetSection("Redis")["Password"]
            });
            services.AddSingleton<IConnectionMultiplexer>(Application.Instance.Redis);
            //services.AddStackExchangeRedisCache(option =>
            //{
            //    option.Configuration = Configuration.GetSection("Redis").Get<RedisConfiguration>();
            //    option.InstanceName = "RedisInstance";
            //});

            services.AddSingleton(_ => new ProductsService());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }
            
            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseExceptionHandler(e =>
                 e.Run(async context =>
                 {
                     var exceptionHandlerPathFeature =
                         context.Features.Get<IExceptionHandlerPathFeature>();
                     //check if the handler path contains api or not.
                     if (exceptionHandlerPathFeature.Path.Contains("api"))
                     {
                         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                         context.Response.ContentType = "text/html";

                         await context.Response.WriteAsync("<html lang=\"tr\"><body>\r\n");
                         await context.Response.WriteAsync("<h3>Bir hata olu�tu!</h3>\r\n");

                         await context.Response.WriteAsync("</body></html>\r\n");
                     }
                     else
                     {
                         context.Response.Redirect("/Error");
                     }
                 })
             );

            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
