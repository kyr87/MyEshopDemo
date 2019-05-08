using MyEshopDemo.Models;
using MyEshopDemo.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshopDemo.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.ToList();
            var listOfOrders = new List<OrdersViewModel>();

            foreach(var order in orders)
            {
                var viewModel = new OrdersViewModel()
                {
                    User = db.Users.SingleOrDefault(u => u.Id == order.UserID),
                    Order = order
                };

                listOfOrders.Add(viewModel);
            }

            return View(listOfOrders);
        }

        public ActionResult Details(int id)
        {
            var order = db.Orders.SingleOrDefault(o => o.ID == id);
            var user = db.Users.SingleOrDefault(u => u.Id == order.UserID);
            var productsWithTheirQuantities = new Dictionary<Product, int>();
            var orderDetails = db.OrdersDetails.Where(od => od.OrderID == order.ID);

            var list = new List<OrderDetails>();

            foreach(var item in orderDetails)
            {
                list.Add(item);
            }

            foreach(var orderDetail in list)
            {
                var product = db.Products.SingleOrDefault(p => p.ID == orderDetail.ProductID);
                var quantity = orderDetail.Quantity;
                productsWithTheirQuantities.Add(product, quantity);
            }

            var viewModel = new OrderDetailsViewModel()
            {
                User = user,
                ProductsWithTheirQuantities = productsWithTheirQuantities
            };

            return View(viewModel);
        }
    }
}