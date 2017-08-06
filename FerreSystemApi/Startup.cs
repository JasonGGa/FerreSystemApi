using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FerreSystemApi.Models;

namespace FerreSystemApi
{
    public class Startup
    {        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connection = @"Server=(localdb)\mssqllocaldb;Database=FerreSystem;Trusted_Connection=True;";
            services.AddDbContext<FerreContext>(opt => opt.UseSqlServer(connection));
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }

        public IConfigurationRoot Configuration { get; }
    }
}