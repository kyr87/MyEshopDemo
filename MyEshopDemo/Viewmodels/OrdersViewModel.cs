using MyEshopDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEshopDemo.Viewmodels
{
    public class OrdersViewModel
    {
        public ApplicationUser User { get; set; }
        public Order Order { get; set; }
    }
}