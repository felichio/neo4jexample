using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Neo4j.Driver;

namespace server
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

            services.AddControllers();
            services.AddScoped<IBlockchainRepository, BlockchainRepository>();
            services.AddSingleton(GraphDatabase.Driver($"neo4j://{Environment.GetEnvironmentVariable("NEO4J_HOST")}:{Environment.GetEnvironmentVariable("NEO4J_PORT")}", AuthTokens.Basic(
                Environment.GetEnvironmentVariable("NEO4J_USER"),
                Environment.GetEnvironmentVariable("NEO4J_PASSWORD")
            )));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
