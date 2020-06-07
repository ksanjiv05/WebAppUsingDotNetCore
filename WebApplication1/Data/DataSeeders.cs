
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data
{
    public class DataSeeders
    {
        private readonly DBWebData dBWebData;
        private readonly IHostingEnvironment hostingEnviroment;
        private readonly UserManager<Users> userManager;

        public DataSeeders(DBWebData dBWebData, IHostingEnvironment hostingEnviroment, UserManager<Users> userManager)
        {
            this.dBWebData = dBWebData;
            this.hostingEnviroment = hostingEnviroment;
            this.userManager = userManager;
        }
        //1/4/2020 modified 
        public async Task Seed()
        {
            dBWebData.Database.EnsureCreated();

            Users User = await userManager.FindByEmailAsync("ksanjiv05@gmail.com");
            if (User == null)
            {
                User = new Users()
                {
                    Email = "ksanjiv0005@gmail.com",
                    UserName = "ksanjiv0005@gmail.com",
                    FirstName = "sanjiv",
                    LastName = "kumar"
                   

                };
            }

            var result = await userManager.CreateAsync(User, "Ksanjiv0005@");
            
            if (result.Succeeded)//you can use IdentityResult.Success
                Console.WriteLine("user created");
            else
                new InvalidOperationException("creating user failed");

            if (!dBWebData.Products.Any())
            {
                var filepath = Path.Combine(hostingEnviroment.ContentRootPath + "/Data/ArtData.json");
                var JsonData = File.ReadAllText(filepath);
                var Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(JsonData);
                dBWebData.AddRange(Products);

                var order = dBWebData.Orders.FirstOrDefault(o => o.Id == 1);
                if (order != null)
                {
                    order.User = User;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem(){

                            Product=Products.First(),
                            UnitPrice=Products.First().Price,
                            Quantity=5

                        }
                    };
                }

                dBWebData.SaveChanges();


            }
        }
    }
}
