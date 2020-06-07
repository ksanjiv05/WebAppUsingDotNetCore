using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//2/4/2020
namespace WebApplication1.Data.Entities
{
    public class Users : IdentityUser
    {
        public string  FirstName { get; set; }
        public string  LastName{ get; set; }
    }
}
