using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using testApi2.Fetchers.CoreContext;
using testApi2.Fetchers;

namespace testApi2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // <--- for extra external confiuguration you are allowed to edit app.Development.setttings or properties/launchSettings.json --->

        // This method gets called by the runtime. Use this method to add services.
        public void ConfigureServices(IServiceCollection services)
        {
            // this is used to add the controllers defined in ./Controllers
            services.AddControllers();
            
            // adding database configs into services using dbcontext and pomelo's sql library
            // the connection string can be found in app.Development.setttings
            services.AddDbContext<ApplicationDbCoreContext>(
                DbContextOptions => DbContextOptions
                    .UseMySql(Configuration.GetConnectionString("Db"), new MySqlServerVersion(new Version(5, 7, 24)))
            );

            // configuring dependency injection for StringStoreFetcher class
            // when StringStoreFetcher is needed all a class has to do is call its interface and the class will be automatically instanciated
            // the interface is the basis of the two classes
            // the fetcher inherits from the interface and thus it is able to be instanciated 
            services.AddScoped<IStringStoreFetcher, StringStoreFetcher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // i havent touched this yet so you on your own here
            // env values are grabbed from ./properties/launchSettings.json
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
