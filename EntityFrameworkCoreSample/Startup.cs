using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCoreSample.Data;
using EntityFrameworkCoreSample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EntityFrameworkCoreSample
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
            services.AddControllersWithViews();

            //services.AddDbContext<MvcMovieContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("MvcMovieContext")));

            var host = Configuration["DBHOST"] ?? "sqlserver";
            var db = Configuration["DBNAME"] ?? "MvcMovieContext";
            var port = Configuration["DBPORT"] ?? "1433";
            var username = Configuration["DBUSERNAME"] ?? "sa";
            var password = Configuration["DBPASSWORD"] ?? "Tien@123";

            //string connStr = $"Data Source={host},{port};Integrated Security=False;";
            //connStr += $"User ID={username};Password={password};Database={db};";
            string connStr = $"Data Source={host},{port};Initial Catalog={db};Integrated Security=False;Persist Security Info=False;User Id={username};Password={password};";
            //connStr += $"Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            services.AddDbContext<MvcMovieContext>(options =>
                options.UseSqlServer(connStr));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //PrepDB.PrepPopulation(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
