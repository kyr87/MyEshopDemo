using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEshopDemo.Models;
using MyEshopDemo.Viewmodels;

namespace MyEshopDemo.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "CanManageProductsOrCategories")]
        public ActionResult New()
        {
            // Get Categories from the database
            var categories = db.Categories.ToList();
            // Init and fill the viewModel
            var viewModel = new ProductFormViewModel
            {
                Product = new Product(),
                Categories = categories
            };
            // Return the apropriate view with the viewModel
            return View("ProductForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            // Get the product from database
            var product = db.Products
                .Include(p => p.Category)
                .SingleOrDefault(p => p.ID == id);

            // Check if the product is null
            if (product == null)
                return HttpNotFound();
            // Get the categories from the database
            var categories = db.Categories.ToList();
            // Init the viewModel and fill it with the queries
            var viewModel = new ProductFormViewModel
            {
                Product = product,
                Categories = categories
            };
            // Return the ProductForm with the viewModel
            if(User.IsInRole("CanManageProductsOrCategories"))
                return View("ProductForm", viewModel);
            return Content("You dont have access");

        }

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).ToList();
            if (User.IsInRole(RoleName.Admin))
                return View(products);
            return View("GuestView", products);
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product product)
        {
            if(product.ImageFile != null)
            {
                product.Thumbnail = Path.GetFileName(product.ImageFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Image/"), product.Thumbnail);
                product.ImageFile.SaveAs(filePath);
            }

            if(product.ID == 0)
            {
                product.Thumbnail = (product.ImageFile == null)
                    ? "na_image.jpg"
                    : product.Thumbnail;

                db.Products.Add(product);
            }
            else
            {
                var productInDb = db.Products.Single(p => p.ID == product.ID);
                productInDb.Name = product.Name;
                productInDb.Description = product.Description;
                productInDb.Featured = product.Featured;
                productInDb.Price = product.Price;
                productInDb.Thumbnail = product.Thumbnail;
                productInDb.CategoryID = product.CategoryID;
            }

            if (!ModelState.IsValid)
            {
                var viewModel = new ProductFormViewModel
                {
                    Product = product,
                    Categories = db.Categories.ToList()
                };
                return View("ProductForm", viewModel);
            }
            else
            {
                db.SaveChanges();
            }

            return RedirectToAction("Index","Products");
        }


        // GET: Admin/Products/Delete/5
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
