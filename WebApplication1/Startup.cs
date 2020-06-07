using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
         
using Microsoft.Extensions.Hosting;
using WebApplication1.Data;
using WebApplication1.Data.Repositery;
using WebApplication1.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using System.Reflection;
using WebApplication1.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApplication1
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //2/4/2020
            services.AddIdentity<Users, IdentityRole>(
                    option => option.User.RequireUniqueEmail = true //here you can modifiy the default options
                    ).AddEntityFrameworkStores<DBWebData>();//To store Data in DBWebData

            //2/4/2020 today all  controller and Data repositry modified and covered type script whic is ts file under wwwroot folder
            services.AddAuthentication()
                    .AddCookie()
                    .AddJwtBearer(cfg => {
                        cfg.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = _configuration["Tokens:Issuer"],
                            ValidAudience = _configuration["Tokens:Audince"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:key"]))
                        };
                    });
            
            //services.AddDbContext<DBWebData>(dbcon =>
            //{
            //    dbcon.UseSqlServer("server=(localdb)\\MSSQLLocalDB;Database=NewDataBase;Integrated Security=True;");
            //});

            services.AddDbContextPool<DBWebData>(entity => entity.UseSqlServer(_configuration.GetConnectionString("EmployeeDbConnection")));
            //services.AddDbContext<DBWebData>(conn =>
            //        conn.UseSqlServer(_configuration.GetConnectionString("Webapplication1DBConnection"))
            //        );

            

            //1/4/2020
            //GetExecutingAssembly()	Gets the assembly that contains the code that is currently executing
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //AppDomain Represents an application domain, which is an isolated environment where applications execute.
            //CurrentDomain	Gets the current application domain for the current Thread.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //-----------------------------------------------
            //services.AddSingleton<DataSeeders>();
            services.AddTransient<DataSeeders>();
            
            
            services.AddTransient<IDBDataRepositry, DBDataRepositry>();
            services.AddSingleton < INullMailService, NullMailService > ();
            //using as pluralshight but gatting error
            //services.AddControllersWithViews().AddJsonOptions(options =>
            //            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);//this method is use to enable mvc
            //alter net
            //services.AddMvc();
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request/response pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) //here you can use env.IsEnvironment("Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
            //this is convantional routing here we use two middel ware to routing 1. UseRouting 2. UseEndPoints
            
            //1/4/2020
           
           
            app.UseRouting();
            app.UseAuthentication();//varify the Identity
            app.UseAuthorization();//Access permission This method is must between Routing and endpoint
            app.UseEndpoints(endpoint=> {
                endpoint.MapControllerRoute(
                    name: "Default",
                    pattern:"{Controller=App}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
