using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using SSFAPP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(SSFAPP.Startup))]

namespace SSFAPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser();
            user.UserName = "mor";
            user.Email = "mor";
            string pass = "lol123";

            var user2 = new ApplicationUser();
            user2.UserName = "ebbe";
            user2.Email = "Esben.laursen@gmail.com";
            string pass2 = "lol123";

            var succes2 = userManager.Create(user2, pass2);
            var succes = userManager.Create(user, pass);

            ConfigureAuth(app);
        }
    }
}
