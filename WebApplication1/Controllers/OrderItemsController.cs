using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositery;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    //1/4/2020
    [Route("/api/orders/{orderid}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {
        private readonly IDBDataRepositry dBRepositry;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public OrderItemsController(IDBDataRepositry dBRepositry, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            this.dBRepositry = dBRepositry;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderItemViewModel>> Get(int orderid)
        {
            try
            {
                if (orderid > 0)
                {
                    var order = dBRepositry.GetOrderById(User.Identity.Name ,orderid);
                    var orderViewModel = mapper.Map<OrderViewModel>(order);
                    if (orderViewModel != null)
                        return Ok(orderViewModel.Items.ToList());
                    else
                        return NotFound();
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Faild to get items" + ex.Message);
                return BadRequest("Faild to get items");
            }
        }
        [HttpGet("{id}")]//map the Item id
        public ActionResult Get(int orderid, int id)
        {
            try
            {
                var item= mapper.Map<OrderViewModel>(dBRepositry.GetOrderById(User.Identity.Name,orderid))
                                                                .Items
                                                                .Where(item => item.Id == id)
                                                                .FirstOrDefault();
                //return item != null ? Ok(item) : NotFound();//why gatting error
                if (item != null)
                    return Ok(item);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError("Faild to get items" + ex.Message);
                return BadRequest("Faild to get items");
            }
        
        }

    }
}