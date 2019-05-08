using MyEshopDemo.Models;
using MyEshopDemo.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshopDemo.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var categories = db.Categories
                .ToList();

            //ViewBag.Categories = categories;

            var products = db.Products
                .Where(p => p.Featured == true)
                .ToList();

            var viewModel = new HomeViewModel
            {
                Categories = categories,
                Products = products
            };

            //ViewBag.Products = products;
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SingleProduct(int id)
        {
            Product product = db.Products
                .Where(p => p.ID == id)
                .SingleOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            Category category = db.Categories
                .Find(product.CategoryID);

            var viewModel = new SingleProductViewModel
            {
                Product = product,
                Category = category
            };

            return View(viewModel);
        }

        public ActionResult SingleCategory(int id)
        {
            Category category = db.Categories
                .SingleOrDefault(c => c.CategoryId == id);

            if(category == null)
            {
                return HttpNotFound();
            }

            var products = db.Products.ToList()
                .Where(p => p.CategoryID == category.CategoryId);

            var viewModel = new SingleCategoryViewModel
            {
                Category = category,
                Products = products
            };

            return View(viewModel);
        }

        public ActionResult Navbar()
        {
            if (User.IsInRole(RoleName.Admin))
            {
                return PartialView("_NavBarAdmin");
            }
            return PartialView("_NavBar");
        }
    }
}