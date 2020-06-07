using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositery;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("Application/json")]
    public class ProductsController : Controller
    {
        private readonly IDBDataRepositry dBDataRepositry;
        private readonly ILogger logger;

        public ProductsController(IDBDataRepositry dBDataRepositry,ILogger<ProductsController> logger)
        {
            this.dBDataRepositry = dBDataRepositry;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesDefaultResponseType]
        //[ProducesErrorResponseType]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            try
            {
                logger.LogInformation("product is called");
                return Ok(dBDataRepositry.GetProducts());
            }
            catch (Exception ex)
            {
                logger.LogInformation("Products is unavailable " + ex.Message);
                return BadRequest("Not Found");
            }
        }

        //[HttpGet]
        //public IActionResult GetProducts()
        //{
        //    try
        //    {

        //        return Ok(dBDataRepositry.GetProducts());
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogInformation("Products is unavailable "+ex.Message);
        //        return BadRequest("Not Found");
        //    }
        //}


        //[HttpGet]
        //public JsonResult GetProducts()
        //{
        //    try
        //    {
        //        logger.LogInformation("return All products");
        //        return Json(dBDataRepositry.GetProducts());
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogInformation("Products not founds " + ex.Message);
        //        return Json("Bad Request");
        //    }
        //}


    }
}