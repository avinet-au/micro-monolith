using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroMonolith.Common;
using MicroMonolith.Personnel;
using MicroMonolith.Setting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private bool MICROSERVICES = false;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            if(MICROSERVICES)
            {
                var baseUris = new Dictionary<MMService, string>();
                baseUris.Add(MMService.Personnel, "http://localhost:52034");
                baseUris.Add(MMService.Setting, "http://localhost:51062");

                services.AddSingleton<ISDKConfigurationService>(provider => new SDKConfigurationService(baseUris));
                services.AddSingleton<ISettingService, SettingSDK>();
                services.AddSingleton<IPersonnelService, PersonnelSDK>();
            } else
            {
                services.AddSingleton<ISettingService, SettingService>();
                services.AddSingleton<IPersonnelService, PersonnelService>();
            }
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
