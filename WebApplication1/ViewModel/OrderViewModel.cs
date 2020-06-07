using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.ViewModel
{
    //1/4/2020
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        public string OrderNumber { get; set; }
        public ICollection<OrderItemViewModel> Items { get; set; }

        
    }
}
