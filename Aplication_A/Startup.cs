using Aplication_A.BL.Interface;
using Aplication_A.BL.Kafka;
using Aplication_A.BL.RabbitMq;
using Aplication_A.BL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Aplication_A
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
            services.Configure<RabbitMqConfig>(Configuration.GetSection(nameof(RabbitMqConfig)));
            services.Configure<KafkaConfig>(Configuration.GetSection(nameof(KafkaConfig))); 

            services.AddSingleton<IRabbitMqService, RabbitMqService>();
            services.AddHostedService<KafkaConsumer>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aplication_A", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aplication_A v1"));
            }

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
