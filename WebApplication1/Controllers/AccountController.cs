using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data.Entities;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    //2/4/2020
    public class AccountController : Controller
    {
        private readonly ILogger logger;
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly IConfiguration configuration;

        public AccountController(ILogger<AccountController> logger,
            SignInManager<Users> signInManager,
            UserManager<Users> userManager,
            IConfiguration configuration
            )
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                ViewBag.UserName = this.User.Identity.Name;
                return RedirectToAction("Index", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                logger.LogInformation("Login Attempt");
                var User= await  signInManager.PasswordSignInAsync(model.UserName,model.Password,model.RemamberMe,false);
                if (User.Succeeded)
                {
                    if(Request.Query.Keys.Contains("ReturnUrl"))
                    {

                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    ViewBag.UserName = model.UserName;
                    return RedirectToAction ("Index", "App");
                }
                else
                    ModelState.AddModelError("", "Invalid UserName or Password !");
                logger.LogInformation("Invalid Login");
                    
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","App");

        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody]LoginViewModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if(user!= null)
            {
                var result =await signInManager.CheckPasswordSignInAsync(user, model.Password,false);
                
                if(result.Succeeded)
                {
                    //create token
                    var Claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString()),//Guid is genarate Unique id
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)//if not add the you can't find name User.Identity.Name it returns null
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:key"]));
                    var cread = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var Token =new  JwtSecurityToken(
                        configuration["Tokens:Issuer"],
                        configuration["Tokens:Audince"],
                        Claims,
                        expires:DateTime.UtcNow.AddMinutes(30),
                        signingCredentials:cread
                        );
                    var Tokenresult = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(Token),
                        expiration = Token.ValidTo
                    };
                   
                    return Created("", Tokenresult);
                }
            }
            ModelState.AddModelError("","Invalid User");
            return BadRequest();
        }
    }
}