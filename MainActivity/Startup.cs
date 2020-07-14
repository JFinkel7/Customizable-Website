/*
 * Project: << Customizable Article Website >>
 * Software Developer: Denis J Finkel
 * Start Date: May 1 , 2020
 * End Date: July 13 , 2020
 * Description: Allow Admin To Regsiter & Login To The Website 
 * Then Allow The Admin To Add, Delete, Update Article Contents
 * -
 * Tools: SQLite, Entity FrameWork Core
 * App Uses: Async-Threading | Cookie Authentication & Authorization | XML File Storage  
 * NOTE: To Use SQL on the cloud Replace SQLite with use SQL-Server 
 * NOTE: To Use App Storage on the cloud Replace XML File With Azure Blob or AWS S3
 */

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MainActivity.Repository;
using MainActivity.Services;
using MainActivity.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.EntityFrameworkCore;

namespace MainActivity {
    public class Startup {
        //***>
        public IConfiguration Configuration { get; }
        //***>

        // Constructor 
        public Startup(IConfiguration configuration) => Configuration = configuration;



        public void ConfigureServices(IServiceCollection services) {
            /*  Adds Cookie Authentication  */
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.Cookie.Name = ".Client";
                    // * This Is The Path That User Will Go To 

                    // Login Path Route 
                    options.LoginPath = "/Home/Index";

                    // Denied Path
                    options.AccessDeniedPath = "/Account/Login";

                    // Login Session Time
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                });



            // (CORE 3.00) Used by the Web Application (MVC) template.
            services.AddControllersWithViews();

            // (CORE 3.00) Equivalent To The Current AddMvc().
            services.AddControllers();

            /* Adds SQLite */
            services.AddEntityFrameworkSqlite().AddDbContext<PrimeDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));


            /*  Adds ArticleContent Transient */
            services.AddTransient<ICompany<ArticleContent>, ArticleContentRepository>();

            /*  Adds Administrator Transient */
            services.AddTransient<ICompany<Administrator>, AdministratorRepository>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }


            /* Use Static Files */
            app.UseStaticFiles();

            /* Use HTTP */
            app.UseHttpsRedirection();

            /* Use Routing */
            app.UseRouting();

            // Who are you?
            app.UseAuthentication();

            // Are you allowed? 
            app.UseAuthorization();

            // Use Cookie Policy 
            app.UseCookiePolicy();

            /* Add Default Route */
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{Controller=Home}/{Action=Index}/{id?}");
            });
        }
    }// CLASS ENDS
}
