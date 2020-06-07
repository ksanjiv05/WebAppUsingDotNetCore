using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication1.Data;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host= CreateHostBuilder(args).Build();
           RunSeeders(host);     
           host.Run();
        }

        private static void RunSeeders(IHost host)
        {
            var seeder=host.Services.GetService<DataSeeders>();
            seeder.Seed().Wait();//beacuse seed is Async
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration(SetupConfig)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetupConfig(HostBuilderContext arg1, IConfigurationBuilder arg2)
        {
            arg2.Sources.Clear();
            arg2.AddJsonFile("DBConfig.json", false, true);//;//Webapplication1DB
                //.AddXmlFile("DBconfig.xml", true)//if you need to config with xml
                //.AddEnvironmentVariables();//if you need to config with enviroment variables
                //if you use all at the same time then the last config is applide if it has data
        }
    }
}
