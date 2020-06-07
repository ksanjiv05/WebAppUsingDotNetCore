using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Repositery
{
    public interface IDBDataRepositry
    {
        public IEnumerable<Product> GetProducts();


        public IEnumerable<Product> GetProductsByCatagories();
        public bool SaveAll();
        public IEnumerable<Order> GetOrders(bool IncludeItems);//1/4/2020
        public Order GetOrderById(int id);//1/4/2020
        public void AddEntity(Object model);//1/4/2020
        void Delete(int id);//1/4
        public IEnumerable<Order> GetOrders(string user, bool includeItems);
        public Order GetOrderById(string name, int orderid);
        void AddOrder(Order newOrder);
    }
}
