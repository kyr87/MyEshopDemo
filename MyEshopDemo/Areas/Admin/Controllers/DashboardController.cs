using MyEshopDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshopDemo.Areas.Admin.Controllers
{
    [Authorize(Roles =RoleName.Admin)]
    public class DashboardController : Controller
    {
        
 
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Navbar()
        {
            ViewBag.messages = db.Messages.Where(m => m.IsRead == false).Count();
            return PartialView("_Navbar");
        }


    }
}