﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BridgetItService.Contracts;
using BridgetItService.Services;
using Microsoft.OpenApi.Models;
using BridgetItService.Settings;
using BridgetItService.MapperFactory;
using BridgetItService.Models;
using ShopifySharp;

namespace BridgetItService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add the IHttpClientFactory service
            services.AddHttpClient();
            services.AddSingleton<ApiHandler>();
            services.AddSingleton<IInfinityPOSClient, InfinityPOSClient>();
            services.AddSingleton<IShopifyServiceAPI, ShopifyServiceAPI>();
            services.AddSingleton<UpdateService>();
            services.AddSingleton<IMap<InfinityPOSProduct, Product>, InfinityToShopifyProductMap>();

            services.AddMvc();
            services.AddControllers();
            services.AddSettingsConfig(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API de BridgetIt",
                    Version = "v1",
                    Description = "API para BridgetItService"
                });
            });
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
    (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de BridgetIt V1");
                });
            }


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
