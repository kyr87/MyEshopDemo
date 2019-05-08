using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using MyEshopDemo.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyEshopDemo.Startup))]
namespace MyEshopDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);           
        }       
    }
}
