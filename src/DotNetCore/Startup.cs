using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DotNetCore.Services;
using DotNetCore.Data;
using Microsoft.EntityFrameworkCore;
using DotNetCore.CustomHandler;
using DotNetCore.Models;
using System.Text;
using System.Security.Cryptography;

namespace DotNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var custentry = Configuration.GetSection("CustEntry").Get<CustEntry>();
            EncryptionMechanism.SetValut(custentry.key);
            // Add framework services.
            services.AddMvc();
            var _conn = Configuration.GetConnectionString("DefaultConnection");
            //var _conn = EncryptionMechanism.AESEncryption(conn);
            var dconn = EncryptionMechanism.AESDecryption(_conn);
            services.AddDbContext<DotnetCoreDbContext>(options => options.UseSqlServer(dconn));
            // Customer Services
            //services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddScoped < IAccountRepository, DatabaseAccountRepository > ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("Home/Error");
            }
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc(route => {
                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
            //app.UseMvcWithDefaultRoute();
        }
    }
}
