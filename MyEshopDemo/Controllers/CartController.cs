using Microsoft.AspNet.Identity;
using MyEshopDemo.Models;
using MyEshopDemo.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MyEshopDemo.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            // Init the cart list
            var cart = Session["cart"]
                as List<CartViewModel>
                ?? new List<CartViewModel>();
            // Check if cart is empty
            if(cart.Count == 0 || Session["cart"] == null)
            {
                ViewBag.Message = "Your cart is empty";
                return View();
            }
            // Calculate the total and save to viewbag
            decimal total = 0;
            foreach(var item in cart)
            {
                total += item.Total;
            }
            ViewBag.GrandTotal = total;
            // Return the view with the list
            return View(cart);
        }

        public ActionResult CartPartial()
        {
            // Init CartViewModel
            CartViewModel model = new CartViewModel();
            // Init Quantity 
            int quantity = 0;
            // Init Price
            decimal price = 0;
            // Init total
            decimal total = 0;
            // Check for cart session
            if(Session["cart"] != null)
            {
                var products = Session["cart"] as List<CartViewModel>;

                // Get total quantity and price
                foreach (var product in products)
                {
                    quantity += model.Quantity;
                    price += product.Quantity * product.Price;
                    total += product.Total;
                }

                model.Quantity = quantity;
                model.Price = price;
                model.GrandTotal = total;
            }
            else
            {
                // Or set quantity and price to 0
                model.Quantity = 0;
                model.Price = 0;
                model.GrandTotal = 0;
            }

            // Return Partialview with the model
            return PartialView(model);
        }

        public ActionResult AddToCartPartial(int id)
        {
            //Init CartViewModel List
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel> ?? new List<CartViewModel>();
            // Init CartViewModel
            CartViewModel model = new CartViewModel();
            // Get the product from the database
            Product product = db.Products.Find(id);

            // Check if product is already in cart
            var productInCart = cart.FirstOrDefault(x => x.ProductId == id);
            //If not add new
            if(productInCart == null)
            {
                cart.Add(new CartViewModel
                {
                    ProductId = product.ID,
                    ProductName = product.Name,
                    Quantity =1,
                    Price = product.Price,
                    Thumbnail = product.Thumbnail
                });
            }
            else
            {
                // Else if is already in cart increment the quantiy
                productInCart.Quantity++;
            }
            // Get quantity total and price and add to model
            int qty = 0;
            decimal price = 0;
            decimal total = 0;

            foreach(var item in cart)
            {
                qty += item.Quantity;
                price += item.Price;
                total += item.Total;
            }

            model.Quantity = qty;
            model.Price = price;
            model.GrandTotal = total;

            // Save cart back to session
            Session["cart"] = cart;
            // Return partial view with model
            return PartialView(model);
        }

        public JsonResult IncrementProduct(int productId)
        {
            //Init cart list
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel>;
            //Get CartViewModel form the list
            CartViewModel model = cart.FirstOrDefault(x => x.ProductId == productId);
            //Increment quantity
            model.Quantity++;
            //Store needed data
            var result = new { qty = model.Quantity, price = model.Price };
            //Return json with data
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult DecrementProduct(int productId)
        {
            //Init cart
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel>;

            //Get model list
            CartViewModel model = cart.FirstOrDefault(x => x.ProductId == productId);
            //Decrement qty
            if (model.Quantity > 1)
            {
                model.Quantity--;
            }
            else
            {
                model.Quantity = 0;
                cart.Remove(model);
            }
            //Store needed data
            var result = new { qty = model.Quantity, price = model.Price };
            //Return json
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void RemoveProduct(int productId)
        {
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel>;
            CartViewModel model = cart.FirstOrDefault(x => x.ProductId == productId);
            cart.Remove(model);
        }

        public ActionResult PaypalPartial()
        {
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel>;
            return PartialView(cart);
        }

        [HttpPost]
        public void PlaceOrder()
        {
            List<CartViewModel> cart = Session["cart"] as List<CartViewModel>;

            string userId = User.Identity.GetUserId();

            Order order = new Order();

            order.UserID = userId;
            order.DateCreated = DateTime.Now;

            db.Orders.Add(order);
            db.SaveChanges();


            OrderDetails orderDetails = new OrderDetails();
            foreach(var item in cart)
            {
                orderDetails.OrderID = order.ID;
                orderDetails.ProductID = item.ProductId;
                orderDetails.Quantity = item.Quantity;

                db.OrdersDetails.Add(orderDetails);
                db.SaveChanges();
            }

            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("00fbc8073e389b", "2d918dffd782ee"),
                EnableSsl = true
            };
            client.Send("admin@example.com", "admin@example.com", "New Order", "You have a new order from " + User.Identity.Name + " check your dashboard for more details");

            Session["cart"] = null;
        }
    }


}