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

            Tuple<string, int> redisConnection = GetRedisConnectionString();
            string serverPassword = GetServerPassword();
            services.AddEasyCaching(options =>
            {
                options.UseRedis(redisConfig => {
                    redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint(redisConnection.Item1, redisConnection.Item2));
                    if (!string.IsNullOrEmpty(serverPassword))
                    {
                        redisConfig.DBConfig.Password = serverPassword;
                    }

                    redisConfig.DBConfig.AllowAdmin = true;
                }, "my-redis");
            });
        }

        public Tuple<string, int> GetRedisConnectionString()
        {
            string[] redisConnection = Configuration.GetConnectionString("RedisConnectionString").Split(":");
            return Tuple.Create(redisConnection[0], Convert.ToInt32(redisConnection[1]));
        }

        public string GetServerPassword()
        {
            return Configuration.GetSection("Server")["serverPassword"];
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
