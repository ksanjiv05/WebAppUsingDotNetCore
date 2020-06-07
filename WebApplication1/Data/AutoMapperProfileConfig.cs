using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;
using WebApplication1.ViewModel;

namespace WebApplication1.Data
{
    //1/4/2020
    public class AutoMapperProfileConfig : Profile
    {
        public AutoMapperProfileConfig()
        {
            CreateMap<Order, OrderViewModel>()// if fields are same then calling this line only orderviewmodel->order
                .ForMember(ov => ov.OrderId, o => o.MapFrom(order => order.Id))//we use formember method when our model and view model fields are differ
                .ReverseMap();//if adding this you can use order->orderviewmodel
            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
        }


    }
}
