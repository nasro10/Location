﻿
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocationVoiture.Startup))]
namespace LocationVoiture
{
    public partial class Startup
    {
       // private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }
        public void CreateDefaultRolesAndUsers()
        {
           // var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
           // var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            //Cree un Admin Par default
            //if (!roleManager.RoleExists("Admin"))
            //{
            //    role.Name = "Admin";
            //    roleManager.Create(role);
            //    ApplicationUser user = new ApplicationUser();
            //    user.UserName = "Nasreddine";
            //    user.Email = "nasreddine1410@hotmail.fr";
            //    var check = userManager.Create(user, "nasro21669940");
            //    if (check.Succeeded)
            //    {
            //        userManager.AddToRole(user.Id, "Admin");
            //    }
            //}
        }
    }
}
