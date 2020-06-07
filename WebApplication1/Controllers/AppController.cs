using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Repositery;
using WebApplication1.Services;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class AppController : Controller
    {
        INullMailService _nullMailService;
        private readonly IDBDataRepositry dBDataRepositry;

        public AppController(INullMailService nullMailService,IDBDataRepositry dBDataRepositry)
        {
            _nullMailService = nullMailService;
            this.dBDataRepositry = dBDataRepositry;
        }
        public IActionResult Index()
        {
            //throw new InvalidOperationException();
            //dBWebData.Products.ToList();
            ViewBag.PageTitle = "Home Page";
            return View();
        }
        [HttpGet("contact")]
        public IActionResult Contact()
        {
            //ViewBag.Title = "This is contact Page ";
            //throw new InvalidOperationException("bro it genrate exception");
            ViewBag.PageTitle = "Contact Us :";
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ViewConatctModel formobj)
        {
            if (ModelState.IsValid)
            {
                _nullMailService.SendMessage(formobj.Email,formobj.Title,formobj.Message);
                ViewBag.Message = "Mail is sent";
                ModelState.Clear();//used to clear model data or you can clear your form
            }
            ViewBag.PageTitle = "Contact Us:";
            return View();
        }
        [HttpGet("about")]
        public IActionResult About()
        {
            ViewBag.PageTitle = "About Us";
       
            return View();
        }


        //[Authorize]
        public IActionResult Shop()
        {
            //return View(dBDataRepositry.GetProducts());
            return View();
        }

        //[Route("{id}")]
        //public IActionResult ViewProduct(int id)
        //{ 
        //    switch(id)
        //    {
        //        case 1 :
        //            return View(dBDataRepositry.GetProducts());
                    
        //        case 2 :
        //            return View(dBDataRepositry.GetProductsByCatagories());
                    

        //    }
        //    return View();
        //}

    }
}

