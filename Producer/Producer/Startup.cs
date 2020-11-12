using System;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Producer
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
            services.AddSingleton<PaymentReceivedProducer>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMassTransit(isp =>
                {
                    var hostConfig = new MassTransitConfiguration();
                    Configuration.GetSection("MassTransit").Bind(hostConfig);

                    return Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        var host = cfg.Host(
                            new Uri(hostConfig.RabbitMqAddress), 
                            h =>
                            {
                                h.Username(hostConfig.UserName);
                                h.Password(hostConfig.Password);
                            });

                        cfg.Durable = hostConfig.Durable;
                        cfg.PurgeOnStartup = hostConfig.PurgeOnStartup;
                    });
                });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}