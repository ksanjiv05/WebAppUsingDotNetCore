using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Repositery
{
    public class DBDataRepositry :IDBDataRepositry
    {
        private readonly DBWebData dBWebData;
        private readonly ILogger<DBDataRepositry> logger;

        public DBDataRepositry(DBWebData dBWebData,ILogger<DBDataRepositry> logger)
        {
            this.dBWebData = dBWebData;
            this.logger = logger;
        }
        //1/4/2020
        public IEnumerable<Order> GetOrders(bool IncludeItems)
        {
            //return dBWebData.Orders.ToList();
            try
            {
                //return IncludeItems == true ? dBWebData.Orders
                //                                    .Include(include => include.Items)
                //                                    .ThenInclude(inc => inc.Product)
                //                                    .ToList()
                //                                    : dBWebData.Orders.ToList();

                if (IncludeItems == true)
                {
                    return dBWebData.Orders
                                        .Include(include => include.Items)
                                        .ThenInclude(inc => inc.Product)
                                        .ToList();
                }
                else
                {
                    return dBWebData.Orders.ToList();
                }


            }
            catch(Exception e)
            {
                logger.LogInformation("Order is occur error "+e.Message);
                return null;
            }
        }
        //5/1/2020
        public IEnumerable<Order> GetOrders(string user, bool includeItems)
        {
            try
            {
                                              

                if (includeItems == true)
                {
                    return dBWebData.Orders
                                        .Where(od=>od.User.UserName==user)
                                        .Include(include => include.Items)
                                        .ThenInclude(inc => inc.Product)
                                        .ToList();
                }
                else
                {
                    return dBWebData.Orders.ToList();
                }


            }
            catch (Exception e)
            {
                logger.LogInformation("Order is occur error " + e.Message);
                return null;
            }

        }


        //1/4/2020
        public Order GetOrderById(int id)
        {
            //return dBWebData.Orders.ToList();
            try
            {
                return dBWebData.Orders
                                .Include(include => include.Items)
                                .ThenInclude(inc => inc.Product)
                                //.Where(o => o.Id == id).FirstOrDefault();
                                .FirstOrDefault(o=>o.Id==id);
            }
            catch (Exception e)
            {
                logger.LogInformation("Order is occur error " + e.Message);
                return null;
            }
        }

        public Order GetOrderById(string name, int orderid)
        {
            try
            {
                return dBWebData.Orders
                                .Include(include => include.Items)
                                .ThenInclude(inc => inc.Product)
                                .Where(o => o.Id == orderid && o.User.UserName==name)
                                .FirstOrDefault();
                                
            }
            catch (Exception e)
            {
                logger.LogInformation("Order is occur error " + e.Message);
                return null;
            }

        }

        public IEnumerable<Product> GetProducts()
        {
            try
            {
                logger.LogInformation("All Product is called");
                return dBWebData.Products
                                .OrderBy(p => p.Id)
                                .Take(15)
                                .ToList();
            }
            catch (Exception ex)
            {
                logger.LogInformation("The error occur when data is retriving "+ex.Message);
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCatagories()
        {

            return dBWebData.Products
                            .OrderBy(p => p.Category)
                            .Take(15)
                            .ToList();

        }

        public bool SaveAll()
        {
            
               
            return dBWebData.SaveChanges()>0;
        }
        public void AddOrder(Order newOrder)
        {
            foreach (var item in newOrder.Items)
            {
                item.Product = dBWebData.Products.Find(item.Product.Id);
            }
            AddEntity(newOrder);
        }

        //1/4/2020
        public void AddEntity(object model)
        {
            dBWebData.Add(model);
            //try
            //{
            //    dBWebData.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
            //    {
            //        // Get entry

            //        DbEntityEntry entry = item.Entry;
            //        string entityTypeName = entry.Entity.GetType().Name;

            //        // Display or log error messages

            //        foreach (DbValidationError subItem in item.ValidationErrors)
            //        {
            //            string message = string.Format("Error '{0}' occurred in {1} at {2}",
            //                     subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
            //            Console.WriteLine(message);
            //        }
            //    }
            //}
        }

        public void Delete(int id)
        {
            try
            {
                dBWebData.Orders.Remove(dBWebData.Orders.FirstOrDefault(od => od.Id == id));
            }
            catch(Exception ex)
            {
                logger.LogError("Error when deleting "+ex.Message);

            }

        }
    }
}
