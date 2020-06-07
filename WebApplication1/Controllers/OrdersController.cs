using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositery;
using WebApplication1.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
{
    //1/4/2020
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IDBDataRepositry dBDataRepositry;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly UserManager<Users> userManager;

        public OrdersController(IDBDataRepositry dBDataRepositry,ILogger<OrdersController> 
            logger, IMapper mapper,
            UserManager<Users> userManager
            )
        {
            this.dBDataRepositry = dBDataRepositry;
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
       
        public ActionResult<IEnumerable<OrderViewModel>> Get(bool includeItems=true)
        {
            
            try {
                var user = User.Identity.Name;
              
                var oders=mapper.Map<IEnumerable<OrderViewModel>>(dBDataRepositry.GetOrders(includeItems));

                //var oders = mapper.Map<IEnumerable<OrderViewModel>>(dBDataRepositry.GetOrders(includeItems));
                logger.LogInformation("order returened");
                return Ok(oders);
            }
            catch(Exception ex)
            {
                logger.LogInformation("Bad Request "+ex.Message);
                return BadRequest("Not found");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                //mapper.Map<Tsource,Tdestination>();
                var result = mapper.Map<Order, OrderViewModel>(dBDataRepositry.GetOrderById(User.Identity.Name,id));   //dBDataRepositry.GetOrderById(id);
                logger.LogInformation("order returened");
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
                
                
            }
            catch (Exception ex)
            {
                logger.LogInformation("Bad Request " + ex.Message);
                return BadRequest("Not found");
            }
        }

        

        [HttpPost]
        //public ActionResult Post(Order order)//it recive only query string
        public async Task<IActionResult> Post([FromBody]OrderViewModel order)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    //    var newOrder = new Order()
                    //    {
                    //        OrderDate = DateTime.MinValue==order.OrderDate?DateTime.UtcNow:order.OrderDate ,
                    //        OrderNumber=order.OrderNumber
                    //    };
                    //    dBDataRepositry.AddEntity(newOrder);
                    //    if(dBDataRepositry.SaveAll())
                    //        return Created("model is created : ", newOrder);

                    if (User.Identity.IsAuthenticated)
                    {
                        logger.LogInformation("auth");
                    }

                   var cuser = User.Identity.Name;
                   var CurrentUser= await userManager.FindByNameAsync(cuser);
                    
                    var newOrder = mapper.Map<OrderViewModel, Order>(order);//here arise exception if you do not configur Order->orderviewmodel so need to config in profile in reverse map
                    newOrder.User = CurrentUser;
                    newOrder.OrderDate= newOrder.OrderDate == DateTime.MinValue ? DateTime.UtcNow : newOrder.OrderDate;

                    dBDataRepositry.AddOrder(newOrder);
                    //dBDataRepositry.AddEntity(newOrder);
                    if (dBDataRepositry.SaveAll())
                        return Created("model is created : ", newOrder);

                }
                return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                logger.LogError("Exception arise when creating model" + ex.Message);
                return BadRequest("Exception arise when creating model");
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                dBDataRepositry.Delete(id);
                if (dBDataRepositry.SaveAll())
                    return Ok();
                else
                    return NoContent();
            }
            else
                return BadRequest();
        }

        //[HttpPost]
        ////public ActionResult Post(Order order)//it recive only query string
        //public IActionResult Post([FromBody]Order order)
        //{
        //    try
        //    {
        //        dBDataRepositry.AddEntity(order);
        //        //bool result = dBDataRepositry.SaveAll();
        //        //if(result)
        //         return Created("model is created : ", order);
        //    }
        //    catch(Exception ex)
        //    {
        //        logger.LogError("faild to save "+ex.Message);
        //        return BadRequest("faild to save");
        //    }

        //}
    }
}