using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration;
using Steeltoe.CloudFoundry.Connector.MySql.EFCore;
using WebAPI.Models;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()

                .AddCloudFoundry();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Optional: Adds IConfigurationRoot as a service and also configures IOption<ConfigServerClientSettingsOptions> 
            // Performs:
            //      services.AddOptions();
            //      services.Configure<ConfigServerClientSettingsOptions>(config);
            //      services.AddSingleton<IConfigurationRoot>(config);
            //services.AddConfigServer(Configuration);

            //// Configure and Add IOptions<OAuthServiceOptions> to the container
            //services.AddOAuthServiceOptions(Configuration);

            // Add framework services.
            services.AddMvc();

            // Add Context and use MySql as provider ... provider will be configured from VCAP_ info
            services.AddDbContext<TreasureContext>(options => options.UseMySql(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            SampleData.InitializeMyContexts(app.ApplicationServices);
        }
    }
}
