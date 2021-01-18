using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core.Configurations;
using Masiv_Roulette.Models;

namespace Masiv_Roulette
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
            RedisConfiguration redisConfiguration = GetRedisConfiguration();
            services.AddEasyCaching(options =>
            {
                options.UseRedis(redisConfig => {
                    redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint(redisConfiguration.Hostname, redisConfiguration.Port));
                    /*if (!string.IsNullOrEmpty(redisConfiguration.ServerPassword))
                    {
                        redisConfig.DBConfig.Password = redisConfiguration.ServerPassword;
                    }*/

                    redisConfig.DBConfig.AllowAdmin = true;
                }, redisConfiguration.Channel);
            });
        }

        public RedisConfiguration GetRedisConfiguration()
        {
            string[] redisConnection = Configuration.GetConnectionString("RedisConnectionString").Split(":");
            return new RedisConfiguration()
            {
                Hostname = redisConnection[0],
                Port = Convert.ToInt32(redisConnection[1]),
                ServerPassword = Configuration.GetSection("Server")["password"],
                Channel = Configuration.GetSection("Server")["channelName"]
            };
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
        }
    }
}
