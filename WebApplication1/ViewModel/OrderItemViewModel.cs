using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//1/4/2020
namespace WebApplication1.ViewModel
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
      
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public int ProductId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSize { get; set; }
       
        public string ProductTitle { get; set; }
        public string ProductArtId { get; set; }
        public string ProductArtist { get; set; }
        // public Order Order { get; set; }//view model is herarical so no need


       
      
    }
}
